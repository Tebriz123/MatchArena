using MatchArena.MVC.ViewModels.Products;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IProductClientService
    {
        Task<List<GetProductItemVM>?> GetAllAsync();
        Task<GetProductVM?> GetByIdAsync(long id);
        Task<bool> CreateAsync(PostProductVM vm);
        Task<bool> UpdateAsync(long id, PutProductVM vm);
        Task<bool> DeleteAsync(long id);

    }
}
