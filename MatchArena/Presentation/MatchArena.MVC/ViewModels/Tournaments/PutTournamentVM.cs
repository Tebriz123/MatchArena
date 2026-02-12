using MatchArena.Domain.Entities;

namespace MatchArena.MVC.ViewModels
{
    public record PutTournamentVM(
        string Name,
        string Description,
        String Address,
        string City,
        string Icon,
        IFormFile Photo,
        DateTime StartTime,
        DateTime EndTime,
        DateTime RegistrationDeadline,
        int MaxTeams,
        int CurrentTeams,
        decimal EntryFee,
        decimal PrizeFund,
        string Format,
        string GameFormat,
        TournamentStatus Status
        );

}
