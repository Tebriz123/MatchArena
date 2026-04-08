using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Sizes;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly ISizeClientService _service;

        public SizeController(ISizeClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var sizes = await _service.GetAllAsync();
            return View(sizes ?? new List<GetSizeItemVM>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostSizeVM vm)
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
            var size = await _service.GetByIdAsync(id);
            if (size is null) return NotFound();

            var vm = new PutSizeVM { Name = size.Name };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, PutSizeVM vm)
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
