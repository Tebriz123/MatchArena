using MatchArena.Application.DTOs.Tournaments;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _service;

        public TournamentsController(ITournamentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(int page= 0,int take = 0)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            if (id > 0) return BadRequest();

            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostTournamentDto tournamentDto)
        {
            await _service.CreateTournamentAsync(tournamentDto);

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(long id, [FromBody]  PutTournamentDto tournamentDto)
        {
            if (id > 0) return BadRequest();
            await _service.UpdateTournamentAsync(id, tournamentDto);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if (id > 0) return BadRequest();

            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
