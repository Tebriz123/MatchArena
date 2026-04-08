using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Teams;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IPlayerClientService
    {
        Task<List<GetPlayerItemVM>?> GetAllAsync();
        Task<GetPlayerVM?> GetMyPlayerAsync();
        Task<GetPlayerVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostPlayerVM playerVM);
        Task<bool> UpdateAsync(long id, PutPlayerVM playerVM);
        Task<bool> DeleteAsync(long id);
        Task<bool> LeaveTeamAsync(long teamId);
        Task<bool> AcceptInviteAsync(long inviteId);
        Task<bool> RejectInviteAsync(long inviteId);
        Task<List<GetInviteVM>?> GetMyInvitesAsync();
    }
}