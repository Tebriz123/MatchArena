using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Player
{
    public record PutPlayerDto(
        string Name,
        string Surname,
        int Age,
        string Information,
        string City,
         string Image,
         int Height,
        IFormFile Photo,
        PlayerPosition Position,
        PlayerLevel Level
        );

}
