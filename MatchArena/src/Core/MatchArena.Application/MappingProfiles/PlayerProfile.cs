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
        opt => opt.MapFrom(p => p.Name))
    .ForCtorParam(nameof(GetPlayerItemDto.Surname),
        opt => opt.MapFrom(p => p.Surname));

            CreateMap<Player, GetPlayerDto>()
                    .ForCtorParam(nameof(GetPlayerDto.Name),
          opt => opt.MapFrom(p => p.Name))       
                    .ForCtorParam(nameof(GetPlayerDto.Surname),
          opt => opt.MapFrom(p => p.Surname))   
                    .ForCtorParam("TeamDtos",
          opt => opt.MapFrom(src => src.PlayerTeams));

            CreateMap<PostPlayerDto, Player>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore())
                 .ForMember(dest => dest.User, opt => opt.Ignore())
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname));

            CreateMap<PutPlayerDto, Player>()
                .ForMember(dest => dest.Image, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<TeamPlayer, GetTeamInPlayerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Team.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Team.Name))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Team.Logo));
        }
    }
}
