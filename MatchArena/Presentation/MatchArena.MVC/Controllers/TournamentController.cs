using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
