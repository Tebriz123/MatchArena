using MatchArena.Domain;

namespace MatchArena.MVC.ViewModels
{
    public record PutPlayerVM(
        string Name,
        string Surname,
        int Age,
        string Information,
        int Height,
        string City,
         string Image,
        IFormFile Photo,
        PlayerPosition Position,
        PlayerLevel Level
        );

}
