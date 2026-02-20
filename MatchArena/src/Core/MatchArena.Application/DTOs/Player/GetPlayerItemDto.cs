using MatchArena.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public record GetPlayerItemDto(
        long Id,
        string Name,
        string Surname,
        int Age,
        string City,
        PlayerPosition Position,
        double Rating,
        int GameCount,
        string Image
        ); 
   
}
