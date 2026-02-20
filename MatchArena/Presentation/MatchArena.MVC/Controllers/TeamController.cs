using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamClientService _teamClient;

        public TeamController(ITeamClientService teamClient)
        {
            _teamClient = teamClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _teamClient.GetAllAsync());
        }

        public async Task<IActionResult> Detail(long id)
        {
            return View(await _teamClient.GetByIdAsync(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostTeamVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _teamClient.CreateAsync(vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var team = await _teamClient.GetByIdAsync(id);
            if (team is null) return NotFound();

            var vm = new PutTeamVM(
                team.Name,
                team.CaptainName,
                null!,   
                team.City,
                team.Information
            );

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, PutTeamVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _teamClient.UpdateAsync(id, vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
