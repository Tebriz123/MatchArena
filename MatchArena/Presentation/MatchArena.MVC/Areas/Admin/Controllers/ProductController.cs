using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Colors;
using MatchArena.MVC.ViewModels.Products;
using MatchArena.MVC.ViewModels.Sizes;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductClientService _service;
        private readonly ICategoryClientService _categoryService;
        private readonly IColorClientService _colorService;
        private readonly ISizeClientService _sizeService;

        public ProductController(
            IProductClientService service,
            ICategoryClientService categoryService,
            IColorClientService colorService,
            ISizeClientService sizeService)
        {
            _service = service;
            _categoryService = categoryService;
            _colorService = colorService;
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            return View(products ?? new List<GetProductItemVM>());
        }

        public async Task<IActionResult> Detail(long id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null) return NotFound();
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            await FillViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                await FillViewBags();
                return View(vm);
            }

            var success = await _service.CreateAsync(vm);
            if (!success)
            {
                ModelState.AddModelError("", "Xəta baş verdi.");
                await FillViewBags();
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null) return NotFound();

            await FillViewBags();

            var vm = new PutProductVM
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, PutProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                await FillViewBags();
                return View(vm);
            }

            var success = await _service.UpdateAsync(id, vm);
            if (!success)
            {
                ModelState.AddModelError("", "Xəta baş verdi.");
                await FillViewBags();
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

        private async Task FillViewBags()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync() ?? new List<GetCategoryItemVM>();
            ViewBag.Colors = await _colorService.GetAllAsync() ?? new List<GetColorItemVM>();
            ViewBag.Sizes = await _sizeService.GetAllAsync() ?? new List<GetSizeItemVM>();
        }
    }
}
