using MatchArena.Application.DTOs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Teams
{
    public record GetTeamDto(
        long Id,
        string Name,
        int PlayerCount,
        string CaptainName,
        string City,
        string Logo,
        ICollection<GetPlayerInTeamDto> PlayerDtos,
        int GameCount,
        int MaxPlayer,         
        double Rating,          
        string Information
        );
}
