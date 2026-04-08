using MatchArena.Application.DTOs.Products;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Colors;
using MatchArena.MVC.ViewModels.Products;
using MatchArena.MVC.ViewModels.Sizes;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Threading.Tasks;

namespace MatchArena.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductClientService _service;
        private readonly ICategoryClientService _categoryService;
        private readonly IColorClientService _colorService;
        private readonly ISizeClientService _sizeService;
        private readonly IProductRatingClientService _ratingService;

        public ProductController(
            IProductClientService service,
            ICategoryClientService categoryService,
            IColorClientService colorService,
            ISizeClientService sizeService,
            IProductRatingClientService ratingService)
        {
            _service = service;
            _categoryService = categoryService;
            _colorService = colorService;
            _sizeService = sizeService;
            _ratingService = ratingService;
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

            var ratings = await _ratingService.GetProductRatingsAsync(id);
            ViewBag.Ratings = ratings ?? new GetProductRatingResponseVM();

            return View(product);
        }

    }
}
