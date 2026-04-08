using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IFieldRatingClientService
    {
        Task<GetFieldRatingResponseVM?> GetFieldRatingsAsync(long fieldId);
        Task<bool> PostRatingAsync(long playerId, long fieldId, PostRatingVM vm);
    }
}
