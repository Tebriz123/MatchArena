using AutoMapper;
using MatchArena.Application.DTOs.Reservations;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class ReservationProfile:Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, GetReservationDto>()
     .ForCtorParam(nameof(GetReservationDto.Id),
         opt => opt.MapFrom(src => src.Id))
     .ForCtorParam(nameof(GetReservationDto.FieldId),
         opt => opt.MapFrom(src => src.FieldId))
     .ForCtorParam(nameof(GetReservationDto.FieldName),
         opt => opt.MapFrom(src => src.Field.Name))
     .ForCtorParam(nameof(GetReservationDto.ReservedTime),
         opt => opt.MapFrom(src => src.ReservedTime))
     .ForCtorParam(nameof(GetReservationDto.ReservedDate),
         opt => opt.MapFrom(src => src.ReservedDate))
     .ForCtorParam(nameof(GetReservationDto.Status),
         opt => opt.MapFrom(src => src.Status))
     .ForCtorParam(nameof(GetReservationDto.Amount),
         opt => opt.MapFrom(src => src.Payment != null ? src.Payment.Amount : 0));
            CreateMap<PostReservationDto, Reservation>();
        }
    }
}
