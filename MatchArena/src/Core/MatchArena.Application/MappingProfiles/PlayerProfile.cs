using AutoMapper;
using MatchArena.Application.DTOs.Player;
using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.MappingProfiles
{
    internal class PlayerProfile: Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, GetPlayerItemDto>()
                .ForCtorParam(nameof(GetPlayerItemDto.Name),
                    opt => opt.MapFrom(p => p.User.Name))
                .ForCtorParam(nameof(GetPlayerItemDto.Surname),
                    opt => opt.MapFrom(p => p.User.Surname));

            CreateMap<Player, GetPlayerDto>()
                .ForCtorParam(nameof(GetPlayerDto.Name),
                    opt => opt.MapFrom(p => p.User.Name))
                .ForCtorParam(nameof(GetPlayerDto.Surname),
                    opt => opt.MapFrom(p => p.User.Surname))
                .ForCtorParam(nameof(GetPlayerDto.TeamDtos),
                    opt => opt.MapFrom(p => p.PlayerTeams
                        .Select(pt => new GetTeamInPlayerDto(pt.Team.Id, pt.Team.Name, pt.Team.Logo))
                        .ToList()));

            CreateMap<PostPlayerDto, Player>()
                .ForMember(dest => dest.Image, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore()); 

            CreateMap<PutPlayerDto, Player>()
                .ForMember(dest => dest.Image, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore()); 

            CreateMap<Player, GetPlayerInTeamDto>()
                .ForCtorParam(nameof(GetPlayerInTeamDto.Name),
                    opt => opt.MapFrom(p => p.User.Name))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Surname),
                    opt => opt.MapFrom(p => p.User.Surname));

            CreateMap<Team, GetTeamInPlayerDto>();
        }
    }
}
