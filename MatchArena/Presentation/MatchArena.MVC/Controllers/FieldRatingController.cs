using MatchArena.Application.Interfaces.Services;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.MVC.Controllers
{
    public class FieldRatingController : Controller
    {
        private readonly IFieldRatingClientService _service;
        private readonly IPlayerClientService _playerClientService;
        public FieldRatingController(IFieldRatingClientService service, IPlayerClientService playerClientService)
        {
            _service = service;
            _playerClientService = playerClientService;
        }
        [HttpPost]
        public async Task<IActionResult> PostRating(long fieldId, PostRatingVM vm)
        {
            var player = await _playerClientService.GetMyPlayerAsync();
            if (player is null)
            {
                TempData["RatingError"] = "Oyunçu tapılmadı.";
                return RedirectToAction("Detail", "Field", new { id = fieldId });
            }

            var success = await _service.PostRatingAsync(player.Id, fieldId, vm);
            TempData[success ? "Success" : "RatingError"] = success
                ? "Reytinqiniz uğurla əlavə edildi."
                : "Xəta baş verdi.";

            return RedirectToAction("Detail", "Field", new { id = fieldId });
        }
    }
}
