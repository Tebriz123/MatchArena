using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels.TournamentRegistrations;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class TournamentRegistrationsController : Controller
    {
        private readonly ITournamentRegistrationClientService _service;

        public TournamentRegistrationsController(ITournamentRegistrationClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Teams(long tournamentId)
        {
            if (tournamentId < 1) return BadRequest();

            var teams = await _service.GetTournamentTeamsAsync(tournamentId);
            if (teams is null) return NotFound();

            ViewBag.TournamentId = tournamentId;
            return View(teams);
        }

        public async Task<IActionResult> Register(long tournamentId)
        {
            var myTeam = await _service.GetMyTeamAsync();

            if (myTeam is null)
            {
                TempData["Error"] = "Turnirə qeydiyyat üçün əvvəlcə komanda yaratmalısınız.";
                return RedirectToAction("Detail", "Tournament", new { id = tournamentId });
            }

            if (!myTeam.IsCaptain)
            {
                TempData["Error"] = "Yalnız komanda kapitanı qeydiyyat edə bilər.";
                return RedirectToAction("Detail", "Tournament", new { id = tournamentId });
            }

            ViewBag.TeamName = myTeam.Name;

            var vm = new PostTournamentRegistrationVM
            {
                TournamentId = tournamentId,
                TeamId = myTeam.Id
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(PostTournamentRegistrationVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _service.RegisterAsync(vm);

            if (result is null)
            {
                ModelState.AddModelError("", "Qeydiyyat zamanı xəta baş verdi.");
                return View(vm);
            }

            var (registrationId, sessionUrl) = result.Value;
            return Redirect(sessionUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(long id, long tournamentId)
        {
            if (id < 1) return BadRequest();

            var success = await _service.CancelAsync(id);

            TempData[success ? "Success" : "Error"] = success
                ? "Qeydiyyat uğurla ləğv edildi."
                : "Ləğv zamanı xəta baş verdi.";

            return RedirectToAction("Team", new { tournamentId });
        }
    }
}