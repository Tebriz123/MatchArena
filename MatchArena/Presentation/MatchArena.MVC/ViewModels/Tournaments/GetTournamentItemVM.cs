using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetTournamentItemVM(
        long id,
        string Name,
        string City,
        string Logo,
        DateTime StartTime,
        DateTime EndTime,
        int CurrentTeams,
        int MaxTeams,
        TournamentStatus Status,
        decimal PrizeFund,
        double Rating,
        ICollection<Tournament> Tournaments
        );

}
