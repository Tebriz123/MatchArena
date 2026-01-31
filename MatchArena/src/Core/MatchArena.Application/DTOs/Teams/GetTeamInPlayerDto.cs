using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Teams
{
    public record GetTeamInPlayerDto(
        long Id,
        string Name,
        string Logo
        );
    
}
