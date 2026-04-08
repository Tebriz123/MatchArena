using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
