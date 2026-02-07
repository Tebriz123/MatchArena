using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;

namespace MatchArena.MVC.ViewModels
{
    public record RegisterVM(
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
