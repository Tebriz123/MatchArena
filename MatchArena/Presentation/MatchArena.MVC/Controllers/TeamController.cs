using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        if (vm.Photo == null)
        {
            ModelState.AddModelError("Photo", "Şəkil seçin.");
            return View(vm);
        }
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

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _teamClient.DeleteAsync(id);
        if (!result) TempData["Error"] = "Komanda silinə bilmədi.";
        else TempData["Success"] = "Komanda silindi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> JoinTeam(long teamId)
    {
        var result = await _teamClient.JoinTeamAsync(teamId);
        if (!result) TempData["Error"] = "Komandaya qoşulmaq mümkün olmadı.";
        else TempData["Success"] = "Komandaya qoşuldunuz.";
        return RedirectToAction("Detail", new { id = teamId });
    }

    [HttpPost]
    public async Task<IActionResult> RemovePlayer(long playerId, long teamId)
    {
        var result = await _teamClient.RemovePlayerAsync(playerId);
        if (!result) TempData["Error"] = "Oyunçu silinə bilmədi.";
        else TempData["Success"] = "Oyunçu komandadan çıxarıldı.";
        return RedirectToAction(nameof(Detail), new { id = teamId });
    }

    [HttpPost]
    public async Task<IActionResult> SendInvite(long teamId, long playerId)
    {
        var result = await _teamClient.SendInviteAsync(teamId, playerId);
        if (!result) TempData["Error"] = "Dəvət göndərilə bilmədi.";
        else TempData["Success"] = "Dəvət göndərildi.";
        return RedirectToAction(nameof(Detail), new { id = teamId });
    }

}