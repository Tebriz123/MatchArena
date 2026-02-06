using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels.Players
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
