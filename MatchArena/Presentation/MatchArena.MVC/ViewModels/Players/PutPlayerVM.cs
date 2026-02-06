using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Players
{
    public class PutPlayerVM(
        string Name,
        string Surname,
        int Age,
        string Description,
        string City,
         string Image,
        IFormFile Photo,
        PlayerPosition Position,
        PlayerLevel Level
        );

}
