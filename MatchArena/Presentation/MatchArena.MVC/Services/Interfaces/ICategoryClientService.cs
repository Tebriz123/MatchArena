using MatchArena.MVC.ViewModels.Category;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface ICategoryClientService
    {
        Task<List<GetCategoryItemVM>?> GetAllAsync();
        Task<GetCategoryVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostCategoryVM categoryVM);
        Task<bool> UpdateAsync(long id, PutCategoryVM categoryVM);
    }
}
