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
            CreateMap<Team, GetTeamDto>()
    .ForCtorParam(nameof(GetTeamDto.CaptainName),
        opt => opt.MapFrom(src => src.TeamPlayers
            .Where(tp => tp.IsCaptain && tp.Player != null)
            .Select(tp => tp.Player.Name + " " + tp.Player.Surname)
            .FirstOrDefault()))
    .ForCtorParam(nameof(GetTeamDto.PlayerDtos),
        opt => opt.MapFrom(src => src.TeamPlayers.Select(tp => tp.Player)));

            CreateMap<PostTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore()); 

            CreateMap<PutTeamDto, Team>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore());


            CreateMap<Player, GetPlayerInTeamDto>()
                .ForCtorParam(nameof(GetPlayerInTeamDto.IsCaptain),
                    opt => opt.MapFrom((src, ctx) =>
                        ctx.Items.ContainsKey("IsCaptain") ? ctx.Items["IsCaptain"] : false));

            CreateMap<TeamPlayer, GetPlayerInTeamDto>()
                .ForCtorParam(nameof(GetPlayerInTeamDto.Id),
                    opt => opt.MapFrom(src => src.Player.Id))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Name),
                    opt => opt.MapFrom(src => src.Player.Name))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Surname),
                    opt => opt.MapFrom(src => src.Player.Surname))
                .ForCtorParam(nameof(GetPlayerInTeamDto.Image),
                    opt => opt.MapFrom(src => src.Player.Image));

            CreateMap<Team, GetTeamItemDto>()
     .ForCtorParam(nameof(GetTeamItemDto.CaptainName),
         opt => opt.MapFrom(src => src.TeamPlayers
             .Where(tp => tp.IsCaptain && tp.Player != null)
             .Select(tp => tp.Player.Name + " " + tp.Player.Surname)
             .FirstOrDefault()));


        }
    }
}

