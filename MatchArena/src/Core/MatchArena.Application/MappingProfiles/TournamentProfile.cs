using AutoMapper;
using MatchArena.Application.DTOs.Teams;
using MatchArena.Application.DTOs.Tournaments;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class TournamentProfile:Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, GetTournamentDto>();

            CreateMap<Tournament, GetTournamentItemDto>();

            CreateMap<PostTournamentDto, Tournament>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore()) 
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.GameDuration, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Teams, opt => opt.Ignore())
                .ForMember(dest => dest.Fields, opt => opt.Ignore());

            CreateMap<PutTournamentDto, Tournament>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore()) 
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.GameDuration, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Teams, opt => opt.Ignore())
                .ForMember(dest => dest.Fields, opt => opt.Ignore());
        }
    }
}
