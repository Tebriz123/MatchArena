using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public class PostPlayerVM(
        string Name,
        string Surname,
        int Age,
        string Image,
        IFormFile Photo,
        string Description,
        string City,
        PlayerPosition Position,
        PlayerLevel Level
        );


}
