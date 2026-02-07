using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;

namespace MatchArena.MVC.ViewModels
{
    public record GetTournamentItemVM(
        long id,
        string Name,
        string City,
        string Icon,
        DateTime StartTime,
        DateTime EndTime,
        int CurrrentTeams,
        int MaxTeams,
        TournamentStatus Status,
        decimal PrizeFund,
        double Rating,
        ICollection<Tournament> Tournaments
        );

}
