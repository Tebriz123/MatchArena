using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class PlayerRatingController : Controller
    {
        private readonly IPlayerRatingClientService _service;

        public PlayerRatingController(IPlayerRatingClientService service)
        {
            _service = service;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostRating(long raterPlayerId, long ratedPlayerId, PostRatingVM vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Reytinq məlumatları düzgün deyil.";
                return RedirectToAction("Details", "Players", new { id = ratedPlayerId });
            }

            var success = await _service.PostRatingAsync(raterPlayerId, ratedPlayerId, vm);

            TempData[success ? "Success" : "Error"] = success
                ? "Reytinqiniz uğurla əlavə edildi."
                : "Xəta baş verdi.";

            return RedirectToAction("Details", "Players", new { id = ratedPlayerId });
        }
    }
}
