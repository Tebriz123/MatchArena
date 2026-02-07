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
            CreateMap<Player, GetPlayerItemDto>();
            CreateMap<Player, GetPlayerDto>()
                .ForCtorParam(nameof(GetPlayerDto.TeamDtos),
                opt => opt.MapFrom(P => P.PlayerTeams
                .Select(pt => new GetTeamInPlayerDto(pt.Team.Id, pt.Team.Name,pt.Team.Logo))
                .ToList()));
                
            CreateMap<PostPlayerDto, Player>();
            CreateMap<PutPlayerDto, Player>();
            CreateMap<Player, GetPlayerInTeamDto>();


        }
    }
}
