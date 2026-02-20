using MatchArena.Domain;
using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetPlayerItemVM(
        long Id,
        string Name,
        string Surname,
        string City,
        int Age,
        PlayerPosition Position,
        double Rating,
        int GameCount,
        string Image,
        ICollection<Player> Players
        );

}
