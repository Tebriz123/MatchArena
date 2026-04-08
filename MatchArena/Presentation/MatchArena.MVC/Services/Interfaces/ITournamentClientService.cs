using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface ITournamentClientService
    {
        Task<List<GetTournamentItemVM>?> GetAllAsync();
        Task<GetTournamentVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostTournamentVM tournamentVM);
        Task<bool> UpdateAsync(long id, PutTournamentVM tournamentVM);
        Task<bool> DeleteAsync(long id);
    }
}
