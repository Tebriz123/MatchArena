using MatchArena.Application.DTOs.TournamentRegistrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface ITournamentRegistrationService
    {
        Task<(long registrationId, string sessionUrl)> RegisterAsync(PostTournamentRegistrationDto dto, string userId);
        Task ConfirmRegistrationAsync(long paymentId);
        Task<IReadOnlyList<GetTournamentRegistrationDto>> GetTournamentTeamsAsync(long tournamentId);
        Task CancelRegistrationAsync(long id, string userId);
    }
}
