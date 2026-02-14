using AutoMapper;
using MatchArena.Application.DTOs.Fields;
using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class FieldProfile:Profile
    {
        public FieldProfile()
        {
            CreateMap<Field, GetFieldDto>()
             .ForCtorParam(nameof(GetFieldDto.Id),
                 opt => opt.MapFrom(f => f.Id))
             .ForCtorParam(nameof(GetFieldDto.Name),
                 opt => opt.MapFrom(f => f.Name))
             .ForCtorParam(nameof(GetFieldDto.City),
                 opt => opt.MapFrom(f => f.City))
             .ForCtorParam(nameof(GetFieldDto.TotalRating),
                 opt => opt.MapFrom(f => f.TotalRating))
             .ForCtorParam(nameof(GetFieldDto.AverageRating),
                 opt => opt.MapFrom(f => f.AverageRating))
             .ForCtorParam(nameof(GetFieldDto.Address),
                 opt => opt.MapFrom(f => f.Address))
             .ForCtorParam(nameof(GetFieldDto.PricePerHour),
                 opt => opt.MapFrom(f => f.PricePerHour))
             .ForCtorParam(nameof(GetFieldDto.StartDate),
                 opt => opt.MapFrom(f => f.StartTime)) 
             .ForCtorParam(nameof(GetFieldDto.EndDate),
                 opt => opt.MapFrom(f => f.EndTime))
             .ForCtorParam(nameof(GetFieldDto.EmptySpace),
                 opt => opt.MapFrom(f => f.EmptySpace));
            CreateMap<Field, GetFieldItemDto>();
            CreateMap<PostFieldDto, Field>();
            CreateMap<PutFieldDto, Field>();
        }
    }
}
