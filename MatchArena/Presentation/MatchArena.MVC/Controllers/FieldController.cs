using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using MatchArena.MVC.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Threading.Tasks;

namespace MatchArena.MVC.Controllers
{
    public class FieldController : Controller
    {
        private readonly IFieldClientService _fieldClient;

        public FieldController(IFieldClientService clientService)
        {
            _fieldClient = clientService;
        }

        public async Task<IActionResult> Index()
        {

          return View(await _fieldClient.GetAllAsync());
        }
        public async Task<IActionResult> Detail(long id)
        {
            return View(await _fieldClient.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostFieldVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

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
                null!, 
                null!,   
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
    }
}
