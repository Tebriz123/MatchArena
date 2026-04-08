using MatchArena.Application.Interfaces.Services;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace MatchArena.MVC.Controllers
{
    public class FieldController : Controller
    {
        private readonly IFieldClientService _fieldClient;
        private readonly IFieldRatingClientService _ratingService;
        public FieldController(IFieldClientService clientService, IFieldRatingClientService ratingService)
        {
            _fieldClient = clientService;
            _ratingService = ratingService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _fieldClient.GetAllAsync());
        }

        public async Task<IActionResult> Detail(long id)
        {
            var field = await _fieldClient.GetByIdAsync(id);
            if (field is null) return NotFound();

            var ratings = await _ratingService.GetFieldRatingsAsync(id);
            field.Ratings = ratings ?? new GetFieldRatingResponseVM();

            return View(field);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostFieldVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (vm.PrimaryPhoto is null)
            {
                ModelState.AddModelError("PrimaryPhoto", "Əsas şəkil seçin.");
                return View(vm);
            }

            var result = await _fieldClient.CreateAsync(vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var field = await _fieldClient.GetByIdAsync(id);
            if (field is null) return NotFound();

            var vm = new PutFieldVM(
                field.Name,
                field.City,
                field.Address,
                null,
                null,
                field.PricePerHour,
                field.StartDate,
                field.EndDate,
                field.Information
            );

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, PutFieldVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _fieldClient.UpdateAsync(id, vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _fieldClient.DeleteAsync(id);
            if (!result) TempData["Error"] = "Meydança silinə bilmədi.";
            else TempData["Success"] = "Meydança silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}