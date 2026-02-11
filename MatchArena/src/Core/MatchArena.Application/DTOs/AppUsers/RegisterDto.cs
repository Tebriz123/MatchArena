using MatchArena.Domain;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.AppUsers
{
    public record RegisterDto(
        string Name,
        string Surname,
        PlayerPosition Position,
        PlayerLevel Level,
        Gender Gender,
        string City,
        string Email,
        string Username,
        string Password
        );
   
}
