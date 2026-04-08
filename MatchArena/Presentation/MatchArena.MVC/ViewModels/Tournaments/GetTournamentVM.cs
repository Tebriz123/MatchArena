using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record GetTournamentVM(
        long Id,
        string Name,
        string City,
        string Address,
        string Logo,
        decimal EntryFee,
        TournamentStatus Status,
        decimal PrizeFund,
        string Description,
        int CurrentTeams,
        int MaxTeams,
        string Format,
        string GameFormat,
        double Rting,
        DateTime StartDate,
        DateTime EndDate,
        DateTime RegistrationDeadline,
        DateTime FinalTime,
        ICollection<Team> Teams
        );

}
