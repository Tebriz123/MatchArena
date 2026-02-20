using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record HomeVM(
    ICollection<GetPlayerItemVM> Players,
    ICollection<GetTeamItemVM> Teams,
    ICollection<GetFieldItemVM> Fields,
    ICollection<GetTournamentItemVM> Tournaments
);
    
}
