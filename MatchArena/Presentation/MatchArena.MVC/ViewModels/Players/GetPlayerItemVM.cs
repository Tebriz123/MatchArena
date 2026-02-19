using MatchArena.Domain;
using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetPlayerItemDto(
        long Id,
        string Name,
        string Surname,
        int Age,
        PlayerPosition Position,
        double Rating,
        int GameCount,
        string Image,
        ICollection<Player> Players
        );

}
