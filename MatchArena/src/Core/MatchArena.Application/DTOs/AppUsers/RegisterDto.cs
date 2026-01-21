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
        string City,
        string Email,
        string UserName,
        string Password,
        string ConfirmPassword
        );
   
}
