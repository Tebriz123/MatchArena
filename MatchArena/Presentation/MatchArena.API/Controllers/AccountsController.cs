using MatchArena.Application.DTOs.AppUsers;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Infrastructure.ForgotPassword;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(IAuthenticationService service, IEmailService emailService, UserManager<AppUser> userManager)
        {
            _service = service;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            await _service.RegisterAsync(registerDto);
            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _service.LoginAsync(loginDto));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Ok();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendPasswordResetAsync(dto.Email, token);

            return Ok("Password reset token has been sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return BadRequest();

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded) return BadRequest();

            return Ok("Password has been reset successfully.");
        }
    }
}
