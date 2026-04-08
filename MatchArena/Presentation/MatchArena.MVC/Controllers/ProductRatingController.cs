using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class ProductRatingController : Controller
    {
        private readonly IProductRatingClientService _service;

        public ProductRatingController(IProductRatingClientService service)
        {
            _service = service;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostRating(long productId, PostRatingVM vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Reytinq məlumatları düzgün deyil.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            var success = await _service.PostRatingAsync(productId, vm);

            TempData[success ? "Success" : "Error"] = success
                ? "Reytinqiniz uğurla əlavə edildi."
                : "Xəta baş verdi.";

            return RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
