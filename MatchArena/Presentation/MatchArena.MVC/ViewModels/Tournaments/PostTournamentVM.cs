using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record PostTournamentVM(
        string Name,
        string Description,
        string Address,
        string City,
        IFormFile Photo,
        DateTime StartDate,
        DateTime EndDate,
        DateTime RegistrationDeadline,
        int MaxTeams,
        decimal EntryFee,
        decimal PrizeFund,
        TournamentStatus Status
        );

}
