using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public class GetPlayerDto(
        long Id,
        string Name,
        string Surname,
        string Description,
        PlayerPosition Position,
        int Age,
        string City,
        ICollection<GetTeamInPlayerDto> TeamDtos,
        int GameCount,
        int Goal
        );

}
