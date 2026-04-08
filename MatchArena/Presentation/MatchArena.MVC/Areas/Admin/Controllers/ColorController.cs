using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Colors;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly IColorClientService _service;

        public ColorsController(IColorClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var colors = await _service.GetAllAsync();
            return View(colors ?? new List<GetColorItemVM>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostColorVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var success = await _service.CreateAsync(vm.Name);
            if (!success)
            {
                ModelState.AddModelError("", "Xəta baş verdi.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var color = await _service.GetByIdAsync(id);
            if (color is null) return NotFound();

            var vm = new PutColorVM { Name = color.Name };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, PutColorVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var success = await _service.UpdateAsync(id, vm.Name);
            if (!success)
            {
                ModelState.AddModelError("", "Xəta baş verdi.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
