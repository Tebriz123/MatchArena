using MatchArena.Application.DTOs.Player;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> PostAsync([FromBody] PostPlayerDto playerDto)
        {
            await _service.CreatePlayerAsync(playerDto);
            return Created();
        }
        public async Task<IActionResult> PutAsync(long id, [FromBody] PutPlayerDto playerDto)
        {
            await _service.UpdateProfileAsync(id, playerDto);
            return NoContent();
        }
    }
}
