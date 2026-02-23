using MatchArena.Application.DTOs.TournamentRegistrations;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
   
    
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentRegistrationsController : ControllerBase
    {
        private readonly ITournamentRegistrationService _service;

        public TournamentRegistrationsController(ITournamentRegistrationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromForm] PostTournamentRegistrationDto dto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null) NotFound();

            var (registrationId, sessionUrl) = await _service.RegisterAsync(dto, userId);
            return Ok(new { registrationId, sessionUrl });
        }

        [HttpGet("{tournamentId}/teams")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTeamsAsync(long tournamentId)
        {
            if (tournamentId < 1) return BadRequest();
            return Ok(await _service.GetTournamentTeamsAsync(tournamentId));
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> CancelAsync(long id)
        {
            if (id < 1) return BadRequest();

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(User is null) NotFound();

            await _service.CancelRegistrationAsync(id, userId);
            return NoContent();
        }
    }
}
