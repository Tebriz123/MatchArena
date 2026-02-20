using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IFieldClientService
    {
        Task<List<GetFieldItemVM>?> GetAllAsync();
        Task<GetFieldVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostFieldVM fieldVM);
        Task<bool> UpdateAsync(long id, PutFieldVM fieldVM);
    }
}
