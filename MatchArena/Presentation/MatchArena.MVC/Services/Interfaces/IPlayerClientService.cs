using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IPlayerClientService
    {
        Task<List<GetPlayerItemVM>?> GetAllAsync();
        Task<GetPlayerVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostPlayerVM playerVM);
        Task<bool> UpdateAsync(long id, PutPlayerVM playerVM);


	}
}
