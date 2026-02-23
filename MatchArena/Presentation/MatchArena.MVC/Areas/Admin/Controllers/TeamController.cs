using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
