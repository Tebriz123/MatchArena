using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryClientService _service;

        public CategoryController(ICategoryClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            return View(categories ?? new List<GetCategoryItemVM>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCategoryVM vm)
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
            var category = await _service.GetByIdAsync(id);
            if (category is null) return NotFound();

            var vm = new PutCategoryVM { Name = category.Name };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, PutCategoryVM vm)
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
