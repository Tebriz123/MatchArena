using MatchArena.Domain;

namespace MatchArena.MVC.ViewModels
{
    public record PutPlayerVM(
       string Name,
       string Surname,
       int Age,
       string Information,
       string City,
        int Height,
       IFormFile Photo,
       PlayerPosition Position,
       PlayerLevel Level
       );

}
