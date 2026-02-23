using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Areas.Admin.Controllers
{
    public class FieldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
