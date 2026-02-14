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
    internal class TeamProfile : Profile
    {
        public TeamProfile()
        {
            // Team -> GetTeamDto (BÜTÜN 11 parametr)
            CreateMap<Team, GetTeamDto>()
                .ForCtorParam(nameof(GetTeamDto.Id),
                    opt => opt.MapFrom(t => t.Id))
                .ForCtorParam(nameof(GetTeamDto.Name),
                    opt => opt.MapFrom(t => t.Name))
                .ForCtorParam(nameof(GetTeamDto.PlayerCount),
                    opt => opt.MapFrom(t => t.TeamPlayers.Count))
                .ForCtorParam(nameof(GetTeamDto.CaptainName),
                    opt => opt.MapFrom(t => t.TeamPlayers
                        .Where(tp => tp.IsCaptain)
                        .Select(tp => tp.Player.User.Name + " " + tp.Player.User.Surname)
                        .FirstOrDefault() ?? "No Captain"))
                .ForCtorParam(nameof(GetTeamDto.City),
                    opt => opt.MapFrom(t => t.City))
                .ForCtorParam(nameof(GetTeamDto.Logo),
                    opt => opt.MapFrom(t => t.Logo))
                .ForCtorParam(nameof(GetTeamDto.PlayerDtos),
                    opt => opt.MapFrom(t => t.TeamPlayers))
                .ForCtorParam(nameof(GetTeamDto.GameCount),
                    opt => opt.MapFrom(t => t.GameCount))
                .ForCtorParam(nameof(GetTeamDto.MaxPlayer),
                    opt => opt.MapFrom(t => t.MaxPlayer))
                .ForCtorParam(nameof(GetTeamDto.Rating),
                    opt => opt.MapFrom(t => t.Rating))
                .ForCtorParam(nameof(GetTeamDto.Information),
                    opt => opt.MapFrom(t => t.Information));

            // Team -> GetTeamItemDto
            CreateMap<Team, GetTeamItemDto>()
                .ForCtorParam(nameof(GetTeamItemDto.Id),
                    opt => opt.MapFrom(t => t.Id))
                .ForCtorParam(nameof(GetTeamItemDto.Name),
                    opt => opt.MapFrom(t => t.Name))
                .ForCtorParam(nameof(GetTeamItemDto.Logo),
                    opt => opt.MapFrom(t => t.Logo))
                .ForCtorParam(nameof(GetTeamItemDto.PlayerCount),
                    opt => opt.MapFrom(t => t.TeamPlayers.Count));

            // TeamPlayer -> GetPlayerInTeamDto
            CreateMap<TeamPlayer, GetPlayerInTeamDto>()
                .ForCtorParam(nameof(GetPlayerInTeamDto.Id),
                    opt => opt.MapFrom(tp => tp.Player.Id))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Name),
                    opt => opt.MapFrom(tp => tp.Player.User.Name))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Surname),
                    opt => opt.MapFrom(tp => tp.Player.User.Surname))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Image),
                    opt => opt.MapFrom(tp => tp.Player.Image))
                .ForCtorParam(nameof(GetPlayerInTeamDto.IsCaptain),
                    opt => opt.MapFrom(tp => tp.IsCaptain));

            // PostTeamDto -> Team
            CreateMap<PostTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore())
                .ForMember(dest => dest.TeamPlayers, opt => opt.Ignore());

            // PutTeamDto -> Team
            CreateMap<PutTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore())
                .ForMember(dest => dest.TeamPlayers, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

