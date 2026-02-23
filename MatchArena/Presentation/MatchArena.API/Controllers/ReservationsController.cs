using MatchArena.Application.DTOs.Reservations;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromForm] PostReservationDto dto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(userId is null) throw new Exception("User is not found");

            var (reservationId, sessionUrl) = await _service.CreateReservationAsync(dto, userId);

            return Ok(new { reservationId, sessionUrl });
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyReservationsAsync()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(userId is null) throw new Exception("User is not found");

            return Ok(await _service.GetUserReservationsAsync(userId));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelAsync(long id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(userId is null) throw new Exception("User is not found");

            await _service.CancelReservationAsync(id, userId);
            return NoContent();
        }
    }
}
