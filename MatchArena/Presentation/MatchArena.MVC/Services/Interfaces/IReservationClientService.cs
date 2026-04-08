using MatchArena.MVC.ViewModels.Reservation;

namespace MatchArena.MVC.Services.Interfaces
{
    public interface IReservationClientService
    {
        Task<(long reservationId, string sessionUrl)?> CreateReservationAsync(PostReservationVM vm);
        Task<List<GetReservationVM>?> GetMyReservationsAsync();
        Task<bool> CancelAsync(long id);
    }
}
