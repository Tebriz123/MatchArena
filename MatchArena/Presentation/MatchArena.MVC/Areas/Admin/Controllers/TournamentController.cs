using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
