using MatchArena.MVC.ViewModels.Category;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface ICategoryClientService
    {
        Task<List<GetCategoryItemVM>?> GetAllAsync();
        Task<GetCategoryVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(string name);
        Task<bool> UpdateAsync(long id, string name);
        Task<bool> DeleteAsync(long id);    
    }
}
