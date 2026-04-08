using MatchArena.Application.DTOs.Teams;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamsController(ITeamService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] PostTeamDto teamDto)
        {
            var userId = _httpContextAccessor.HttpContext?
                .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(userId is null) NotFound();

            await _service.CreateTeamAsync(teamDto, userId);
            return Created();
        }

        [Authorize]
        [HttpPost("{teamId}/join")]
        public async Task<IActionResult> JoinTeamAsync(long teamId)
        {
            if (teamId < 1) return BadRequest();

            var userId = _httpContextAccessor.HttpContext?
                .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(userId is null) NotFound();

            await _service.AddPlayerToTeamAsync(teamId, userId);
            return Ok();
        }


        [HttpPost("{teamId}/invite/{playerId}")]
        [Authorize]
        public async Task<IActionResult> SendInvite(long teamId, long playerId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            await _service.SendInviteAsync(teamId, playerId, userId);
            return Ok();
        }

        [HttpDelete("players/{playerId}")]
        [Authorize]
        public async Task<IActionResult> RemovePlayer(long playerId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return BadRequest();

            await _service.RemovePlayerFromTeamAsync(playerId, userId);

            return NoContent();
        }



        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAsync(long id, [FromForm] PutTeamDto teamDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateTeamAsync(id, teamDto); 
            return NoContent();
        }


        [HttpDelete]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if(id < 1) return BadRequest();
            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
