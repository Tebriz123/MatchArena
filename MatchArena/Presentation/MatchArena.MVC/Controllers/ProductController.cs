using MatchArena.Application.DTOs.Products;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Threading.Tasks;

namespace MatchArena.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductClientService _productClient;

        public ProductController(IProductClientService productClient)
        {
            _productClient = productClient;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _productClient.GetAllAsync());
        }

        public async Task<IActionResult> Detail(long id)
        {
            return View(await _productClient.GetByIdAsync(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostProductVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _productClient.CreateAsync(vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var product = await _productClient.GetByIdAsync(id);
            if (product is null) return NotFound();

            var vm = new PutProductDto(
                product.Name,
                product.Price,
                product.Description,
                product.CategoryDto.Id,
                null!, 
                null!, 
                product.SizeDtos.Select(s => s.Id).ToList(),
                product.ColorDtos.Select(c => c.Id).ToList()
            );

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, PutProductVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _productClient.UpdateAsync(id, vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
