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
        public async Task<IActionResult> PostAsync([FromForm] PostPlayerDto playerDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated");

            await _service.CreatePlayerAsync(playerDto, userId); 
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(long id, [FromForm] PutPlayerDto playerDto)
        {
            if(id<1) return BadRequest();
            await _service.UpdatePlayerAsync(id, playerDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if(id< 1) return BadRequest();
            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
