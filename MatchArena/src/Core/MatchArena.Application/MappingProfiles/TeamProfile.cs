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
           .ForCtorParam(nameof(GetTeamDto.CaptainName), opt => opt.MapFrom(src =>
               src.Player != null && src.Player.User != null
                   ? $"{src.Player.User.Name} {src.Player.User.Surname}"
                   : "No Captain"))
           .ForCtorParam(nameof(GetTeamDto.PlayerDtos), opt => opt.MapFrom(src =>
               src.TeamPlayers.Select(tp => new GetPlayerInTeamDto(
                   tp.Player.Id,
                   tp.Player.User.Name,
                   tp.Player.User.Surname,
                   tp.Player.Image
               )).ToList()));

            CreateMap<Team, GetTeamItemDto>()
                .ForCtorParam(nameof(GetTeamItemDto.CaptainName), opt => opt.MapFrom(src =>
                    src.Player != null && src.Player.User != null
                        ? $"{src.Player.User.Name} {src.Player.User.Surname}"
                        : "No Captain"))
                .ForCtorParam(nameof(GetTeamItemDto.Logo), opt => opt.MapFrom(src => src.Logo));

            CreateMap<Team, GetTeamInPlayerDto>();

            CreateMap<PostTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore())
                .ForMember(dest => dest.CaptainId, opt => opt.Ignore())
                .ForMember(dest => dest.Player, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<PutTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore())
                .ForMember(dest => dest.CaptainId, opt => opt.Ignore())
                .ForMember(dest => dest.Player, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
    }

