using MatchArena.Application.DTOs.Player;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Teams
{
    public record GetTeamItemDto(
        long id,
        string Name,
        int PlayerCount,
        string City,
        int GameCount,
        TeamPlayer Capitain,
        string Image
        );
  
}
