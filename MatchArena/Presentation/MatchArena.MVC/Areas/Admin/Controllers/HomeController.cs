using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
