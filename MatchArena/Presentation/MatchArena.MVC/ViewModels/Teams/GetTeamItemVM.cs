using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetTeamItemVM(
        long id,
        string Name,
        int PlayerCount,
        int MaxPlayer,
        string City,
        int GameCount,
        TeamPlayer Captain,
        string Image,
        double Rating,
        ICollection<Team> Teams
        );

}
