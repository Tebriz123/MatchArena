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
    internal class TeamProfile:Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, GetTeamDto>()
               .ForCtorParam(nameof(GetTeamDto.PlayerDtos),
               opt=>opt.MapFrom(t=>t.TeamPlayers
               .Select(tp=>new GetPlayerInTeamDto(tp.Player.Id,tp.Player.User.Name,tp.Player.User.Surname,tp.Player.Image))
               .ToList()));

            CreateMap<Team, GetTeamInPlayerDto>();

            CreateMap<Team, GetTeamItemDto>()
                .ForCtorParam(nameof(GetTeamItemDto.CapitainName),
                opt=>opt.MapFrom(t=>t.Player.User.Name));

            CreateMap<PostTeamDto, Team>()
            .ForMember(dest => dest.Logo, opt => opt.Ignore());

            CreateMap<PutTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore());
        }
    }
}
