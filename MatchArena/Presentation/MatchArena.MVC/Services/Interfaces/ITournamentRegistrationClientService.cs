using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.TournamentRegistrations;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface ITournamentRegistrationClientService
    {
        Task<(long registrationId, string sessionUrl)?> RegisterAsync(PostTournamentRegistrationVM vm);
        Task<List<GetTournamentRegistrationVM>?> GetTournamentTeamsAsync(long tournamentId);
        Task<bool> CancelAsync(long id);
        Task<GetTeamVM?> GetMyTeamAsync();
    }
}
