using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public record PostPlayerDto(
        string Name,
        string Surname,
        int Age,
        string Image,
        IFormFile Photo, 
        string Information,
        string City,
        PlayerPosition Position,
        PlayerLevel Level
        );

  
}
