using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    using global::MatchArena.MVC.Services.Interfaces;
    using global::MatchArena.MVC.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    namespace MatchArena.MVC.Areas.Admin.Controllers
    {
        [Area("Admin")]
        public class PlayerController : Controller
        {
            private readonly IPlayerClientService _playerClientService;

            public PlayerController(IPlayerClientService playerClientService)
            {
                _playerClientService = playerClientService;
            }

            public async Task<IActionResult> Index()
            {
                var players = await _playerClientService.GetAllAsync();
                return View(players ?? new List<GetPlayerItemVM>());
            }
        }
    }
}
