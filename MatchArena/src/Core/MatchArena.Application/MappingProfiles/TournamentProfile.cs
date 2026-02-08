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
            CreateMap<Tournament, GetTournamentItemDto>();
            CreateMap<Tournament, GetTournamentDto>();
            CreateMap<PostTournamentDto, Tournament>();
            CreateMap<PutTournamentDto, Tournament>();
        }
    }
}
