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
        [HttpPut]
        public async Task<IActionResult> PutAsync(long id, [FromBody] PutPlayerDto playerDto)
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
