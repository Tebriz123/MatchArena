using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public record GetPlayerItemDto(
        long Id,
        string FullName,
        int Age,
        PlayerPosition Position,
        double Raiting,
        int GameCount,
        string Image
        ); 
   
}
