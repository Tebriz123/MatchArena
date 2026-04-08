using MatchArena.MVC.ViewModels.Colors;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IColorClientService
    {
        Task<List<GetColorItemVM>?> GetAllAsync();
        Task<GetColorVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(string name);
        Task<bool> UpdateAsync(long id, string name);
        Task<bool> DeleteAsync(long id);
    }
}
