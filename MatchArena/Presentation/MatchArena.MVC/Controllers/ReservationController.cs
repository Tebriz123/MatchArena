using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationClientService _service;

        public ReservationController(IReservationClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> MyReservations()
        {
            var reservations = await _service.GetMyReservationsAsync();
            return View(reservations ?? new List<GetReservationVM>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostReservationVM vm)
        {
            var result = await _service.CreateReservationAsync(vm);

            if (result is null)
            {
                TempData["Error"] = "Rezervasiya zamanı xəta baş verdi.";
                return RedirectToAction("Details", "Fields", new { id = vm.FieldId });
            }

            var (reservationId, sessionUrl) = result.Value;
            return Redirect(sessionUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(long id)
        {
            var success = await _service.CancelAsync(id);

            TempData[success ? "Success" : "Error"] = success
                ? "Rezervasiya uğurla ləğv edildi."
                : "Ləğv zamanı xəta baş verdi.";

            return RedirectToAction(nameof(MyReservations));
        }
    }
}
