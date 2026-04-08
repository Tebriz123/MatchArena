using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IProductRatingClientService
    {
        Task<GetProductRatingResponseVM?> GetProductRatingsAsync(long productId);
        Task<bool> PostRatingAsync(long productId, PostRatingVM vm);
    }
}
