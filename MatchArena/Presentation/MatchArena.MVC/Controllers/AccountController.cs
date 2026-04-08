using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.AppUsers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MatchArena.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountClientService _accountService;

        public AccountController(IAccountClientService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _accountService.RegisterAsync(model);
            if (!success)
            {
                ModelState.AddModelError("", "Qeydiyyat alınmadı. Email və ya istifadəçi adı artıq mövcuddur.");
                return View(model);
            }

            TempData["Success"] = "Qeydiyyat uğurla tamamlandı!";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var tokenResponse = await _accountService.LoginAsync(model);
            if (tokenResponse is null)
            {
                ModelState.AddModelError("", "İstifadəçi adı/email və ya şifrə yanlışdır.");
                return View(model);
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenResponse.Token);

            var identity = new ClaimsIdentity(
                jwt.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = jwt.ValidTo
                });

            Response.Cookies.Append("jwtToken", tokenResponse.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = jwt.ValidTo
            });

            TempData["Success"] = $"Xoş gəldiniz, {tokenResponse.UserName}!";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _accountService.ForgotPasswordAsync(model);
            TempData["Success"] = "Əgər bu email mövcuddursa, sıfırlama linki göndəriləcək.";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
            => View(new ResetPasswordVM { Email = email, Token = token });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _accountService.ResetPasswordAsync(model);
            if (!success)
            {
                ModelState.AddModelError("", "Şifrə dəyişdirilmədi. Token etibarsız və ya müddəti bitib.");
                return View(model);
            }

            TempData["Success"] = "Şifrəniz uğurla dəyişdirildi!";
            return RedirectToAction("Login");
        }
    }
}