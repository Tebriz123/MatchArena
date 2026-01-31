using MatchArena.Application.DTOs.Teams;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamsController(ITeamService service)
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
        public async Task<IActionResult> PostAsync([FromBody] PostTeamDto teamDto)
        {
            await _service.CreateTeamAsync(teamDto);
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(long id, [FromBody] PutTeamDto teamDto)
        {
            await _service.UpdateTeamAsync(id, teamDto);
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if(id < 1) return BadRequest();
            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
