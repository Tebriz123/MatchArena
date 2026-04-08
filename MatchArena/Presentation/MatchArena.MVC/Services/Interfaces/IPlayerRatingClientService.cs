using MatchArena.MVC.ViewModels;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IPlayerRatingClientService
    {
        Task<GetPlayerRatingResponseVM?> GetPlayerRatingsAsync(long playerId);
        Task<bool> PostRatingAsync(long raterPlayerId, long ratedPlayerId, PostRatingVM vm);
    }
}
