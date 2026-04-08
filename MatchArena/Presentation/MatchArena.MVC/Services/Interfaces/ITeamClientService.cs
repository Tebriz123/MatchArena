using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface ITeamClientService
    {
        Task<List<GetTeamItemVM>?> GetAllAsync();
        Task<GetTeamVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostTeamVM teamVM);
        Task<bool> UpdateAsync(long id, PutTeamVM teamVM);
        Task<bool> DeleteAsync(long id);
        Task<bool> JoinTeamAsync(long teamId);
        Task<bool> RemovePlayerAsync(long playerId);
        Task<bool> SendInviteAsync(long teamId, long playerId);
     
    }
};

