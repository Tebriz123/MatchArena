using MatchArena.Domain;
using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetPlayerItemVM(
        long Id,
        string FullName,
        int Age,
        PlayerPosition Position,
        double Raiting,
        int GameCount,
        string Image,
        ICollection<Player> Players
        );


}
