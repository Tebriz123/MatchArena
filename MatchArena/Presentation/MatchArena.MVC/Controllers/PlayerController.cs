using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerClientService _playerClient;
        private readonly IPlayerRatingClientService _ratingService;
        private readonly ITeamClientService _teamClient;

        public PlayerController(IPlayerClientService playerClient, IPlayerRatingClientService ratingService, ITeamClientService teamClient)
        {
            _playerClient = playerClient;
            _ratingService = ratingService;
            _teamClient = teamClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _playerClient.GetAllAsync());
        }

        public async Task<IActionResult> Detail(long id)
        {
            var player = await _playerClient.GetByIdAsync(id);
            if (player is null) return NotFound();

            var ratings = await _ratingService.GetPlayerRatingsAsync(id);
            ViewBag.Ratings = ratings ?? new GetPlayerRatingResponseVM();

            if (User.Identity?.IsAuthenticated == true)
            {
                var allTeams = await _teamClient.GetAllAsync();
                var myUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var myCaptainTeam = allTeams?.FirstOrDefault(t => t.CaptainUserId == myUserId);
                ViewBag.CaptainTeamId = myCaptainTeam?.Id;
            }

            return View(player);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostPlayerVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var result = await _playerClient.CreateAsync(vm);
                if (!result)
                {
                    ModelState.AddModelError("", "Artıq profiliniz mövcuddur.");
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Xəta: {ex.Message}");
                return View(vm);
            }
        }

        [HttpGet]
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
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _playerClient.DeleteAsync(id);
            if (!result)
            {
                TempData["Error"] = "Silinmə zamanı xəta baş verdi.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveTeam(long teamId)
        {
            var result = await _playerClient.LeaveTeamAsync(teamId);
            if (!result)
            {
                TempData["Error"] = "Komandadan çıxma zamanı xəta baş verdi.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyInvites()
        {
            var invites = await _playerClient.GetMyInvitesAsync();
            return View(invites);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptInvite(long inviteId, string? returnUrl)
        {
            var result = await _playerClient.AcceptInviteAsync(inviteId);
            if (!result)
                TempData["Error"] = "Dəvəti qəbul etmək mümkün olmadı.";
            else
                TempData["Success"] = "Dəvəti qəbul etdiniz.";

            return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(MyInvites));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectInvite(long inviteId, string? returnUrl)
        {
            var result = await _playerClient.RejectInviteAsync(inviteId);
            if (!result)
                TempData["Error"] = "Dəvəti rədd etmək mümkün olmadı.";
            else
                TempData["Success"] = "Dəvəti rədd etdiniz.";

            return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(MyInvites));
        }
    }
}