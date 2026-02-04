using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
