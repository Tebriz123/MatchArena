using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public record GetPlayerDto(
        long Id,
        string Image,
        string Name,
        string Surname,
        int Height,
        string Information,
        PlayerPosition Position,
        int Age,
        string City,
        ICollection<GetTeamInPlayerDto> TeamDtos,
        int GameCount,
        int Goal
        );

}
