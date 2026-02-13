using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public record GetPlayerInTeamDto(
        long Id,
        string Name,
        string Surname,
        string Image,
        bool IsCaptain
        );
  
}
