using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record PostPlayerVM(
        string Name,
        string Surname,
        int Age,
        int Height,
        string Image,
        IFormFile Photo,
        string Information,
        string City,
        PlayerPosition Position,
        PlayerLevel Level
        );


}
