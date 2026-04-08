using MatchArena.MVC.ViewModels.Sizes;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface ISizeClientService
    {
        Task<List<GetSizeItemVM>?> GetAllAsync();
        Task<GetSizeVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(string name);
        Task<bool> UpdateAsync(long id, string name);
        Task<bool> DeleteAsync(long id);
    }
}
