using MatchArena.Application.DTOs.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<(long reservationId, string sessionUrl)> CreateReservationAsync(PostReservationDto dto, string userId);
        Task ConfirmReservationAsync(long paymentId);
        Task<IReadOnlyList<GetReservationDto>> GetUserReservationsAsync(string userId);
        Task CancelReservationAsync(long id, string userId);
    }
}
