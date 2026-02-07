using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record HomeVM(
        ICollection<Player> Players,
        ICollection<Team> Teams,
        ICollection<Field> Fields,
        ICollection<Tournament> Tournaments
        );
    
}
