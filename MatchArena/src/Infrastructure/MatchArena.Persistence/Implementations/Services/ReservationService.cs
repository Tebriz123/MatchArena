using AutoMapper;
using MatchArena.Application.DTOs.Reservations;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class ReservationService:IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public ReservationService(
            IReservationRepository reservationRepository,
            IFieldRepository fieldRepository,
            IPaymentService paymentService,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _fieldRepository = fieldRepository;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<(long reservationId, string sessionUrl)> CreateReservationAsync(
            PostReservationDto dto, string userId)
        {
            Field field = await _fieldRepository.GetByIdAsync(dto.FieldId);
            if (field is null)
                throw new Exception("Field is not found");

            if (!field.EmptySpace.Contains(dto.ReservedTime))
                throw new Exception("This time is already reserved or unavailable.");

            bool alreadyReserved = _reservationRepository.GetAll(
                r => r.FieldId == dto.FieldId &&
                     r.ReservedTime == dto.ReservedTime &&
                     r.ReservedDate.Date == dto.ReservedDate.Date &&
                     r.Status != ReservationStatus.Cancelled
            ).Any();

            if (alreadyReserved)
                throw new Exception("This time was reserved");

            var reservation = new Reservation
            {
                UserId = userId,
                FieldId = dto.FieldId,
                ReservedTime = dto.ReservedTime,
                ReservedDate = dto.ReservedDate,
                Status = ReservationStatus.Pending
            };

            _reservationRepository.Add(reservation);
            await _reservationRepository.SaveChangesAsync();

            var (payment, sessionUrl) = await _paymentService.InitiatePaymentAsync(
     userId, PaymentType.Field, reservation.FieldId);

            reservation.PaymentId = payment.Id;
            _reservationRepository.Update(reservation);
            await _reservationRepository.SaveChangesAsync();

            return (reservation.Id, sessionUrl);
        }

        public async Task ConfirmReservationAsync(long paymentId)
        {
            var reservation = _reservationRepository.GetAll(
                r => r.PaymentId == paymentId
            ).FirstOrDefault();

            if (reservation is null) return;

            Field field = await _fieldRepository.GetByIdAsync(reservation.FieldId);
            if (field is null) return;

            var timeToRemove = field.EmptySpace.FirstOrDefault(t => t == reservation.ReservedTime);
            if (timeToRemove != default)
            {
                field.EmptySpace.Remove(timeToRemove);
            }

            reservation.Status = ReservationStatus.Confirmed;

            _reservationRepository.Update(reservation);
            _fieldRepository.Update(field);
            await _reservationRepository.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<GetReservationDto>> GetUserReservationsAsync(string userId)
        {
            var reservations = await _reservationRepository.GetAll(
                func: r => r.UserId == userId,
                includes: "Field"
            ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetReservationDto>>(reservations);
        }

        public async Task CancelReservationAsync(long id, string userId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id, "Field");
            if (reservation is null)
                throw new Exception("No reservation found");

            if (reservation.UserId != userId)
                throw new Exception("This reservation does not belong to you.");

            if (reservation.Status == ReservationStatus.Confirmed)
            {

                reservation.Field.EmptySpace.Add(reservation.ReservedTime);
                _fieldRepository.Update(reservation.Field);
            }

            reservation.Status = ReservationStatus.Cancelled;
            _reservationRepository.Update(reservation);
            await _reservationRepository.SaveChangesAsync();
        }
    }
}
