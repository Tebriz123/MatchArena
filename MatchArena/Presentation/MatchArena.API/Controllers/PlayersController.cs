using MatchArena.Application.DTOs.Player;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _service;

        public PlayersController(IPlayerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int page = 0, int take = 0)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            if (id < 1) return BadRequest();
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> PostAsync([FromForm] PostPlayerDto playerDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            bool exists = await _service.PlayerExistsAsync(userId);
            if (exists)
                return BadRequest("This user already has a profile.");

            await _service.CreatePlayerAsync(playerDto, userId);
            return Created();
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutAsync(long id, [FromForm] PutPlayerDto playerDto)
        {
            if(id<1) return BadRequest();
            await _service.UpdatePlayerAsync(id, playerDto);
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if(id< 1) return BadRequest();
            await _service.RemoveAsync(id);
            return NoContent();
        }


        [HttpDelete("leave/{teamId}")]
        [Authorize]
        public async Task<IActionResult> LeaveTeamAsync(long teamId)
        {
            if (teamId < 1) return BadRequest();

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            await _service.LeaveTeamAsync(teamId, userId);
            return NoContent();
        }
        [HttpPost("invites/{inviteId}/accept")]
        [Authorize]
        public async Task<IActionResult> AcceptInvite(long inviteId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return NotFound();

            await _service.AcceptInviteAsync(inviteId, userId);
            return Ok();
        }

        [HttpPost("invites/{inviteId}/reject")]
        [Authorize]
        public async Task<IActionResult> RejectInvite(long inviteId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return NotFound();

            await _service.RejectInviteAsync(inviteId, userId);
            return Ok();
        }
        [HttpGet("my-invites")]
        [Authorize]
        public async Task<IActionResult> GetMyInvites()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return NotFound();
            var invites = await _service.GetMyInvitesAsync(userId);
            return Ok(invites);
        }

    }
}
