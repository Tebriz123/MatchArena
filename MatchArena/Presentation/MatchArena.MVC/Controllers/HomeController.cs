using MatchArena.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MatchArena.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
