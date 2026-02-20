using MatchArena.Application.Interfaces.Repositories;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerClientService _playerClient;

        public PlayerController(IPlayerClientService playerClient)
        {
            _playerClient = playerClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _playerClient.GetAllAsync());
        }

        public async Task<IActionResult> Detail(long id)
        {
            return View(await _playerClient.GetByIdAsync(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostPlayerVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _playerClient.CreateAsync(vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var player = await _playerClient.GetByIdAsync(id);
            if (player is null) return NotFound();
            var vm = new PutPlayerVM(
                            player.Name,
                            player.Surname,
                            player.Age,
                            player.Information,
                            player.City,
                            player.Height,
                            player.Image,
                            null!,
                            player.Position,
                            player.Level
            );


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, PutPlayerVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _playerClient.UpdateAsync(id, vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
