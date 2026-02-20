using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentClientService _tournamentClient;

        public TournamentController(ITournamentClientService tournamentClient)
        {
            _tournamentClient = tournamentClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _tournamentClient.GetAllAsync());
        }

        public async Task<IActionResult> Detail(long id)
        {
            return View(await _tournamentClient.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostTournamentVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _tournamentClient.CreateAsync(vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long id)
        {
            var tournament = await _tournamentClient.GetByIdAsync(id);
            if (tournament is null) return NotFound();

            var vm = new PutTournamentVM(
                tournament.Name,
                tournament.Description,
                tournament.Address,
                tournament.City,
                tournament.Logo,
                null!,
                tournament.StartTime,
                tournament.EndTime,
                tournament.RegistrationDeadline,
                tournament.MaxTeams,
                tournament.CurrentTeams,
                tournament.EntryFee,
                tournament.PrizeFund,
                tournament.Format,
                tournament.GameFormat,
                tournament.Status
            );

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, PutTournamentVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _tournamentClient.UpdateAsync(id, vm);
            if (!result)
            {
                ModelState.AddModelError("", "Xəta baş verdi, yenidən cəhd edin.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        } 
    }
}
