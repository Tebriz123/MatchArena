using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClientService _clientService;

        public AccountController(IAccountClientService clientService)
        {
            _clientService = clientService;
        }
         
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var token = await _clientService.LoginAsync(loginVM);
            if (token == null)
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View(loginVM);
            }

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var result = await _clientService.RegisterAsync(registerVM);
            if (!result)
            {
                ModelState.AddModelError("", "Qeydiyyat zamanı xəta baş verdi.");
                return View(registerVM);
            } 

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction(nameof(Login));
        }
    }
}
