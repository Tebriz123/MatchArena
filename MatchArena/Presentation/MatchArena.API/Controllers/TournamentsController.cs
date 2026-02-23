using MatchArena.Application.DTOs.Tournaments;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _service;

        public TournamentsController(ITournamentService service)
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
        public async Task<IActionResult> PostAsync([FromForm] PostTournamentDto tournamentDto)
        {
            await _service.CreateTournamentAsync(tournamentDto);
            return Created();
        }

        [HttpPut("{id}")]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> PutAsync(long id, [FromForm] PutTournamentDto tournamentDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateTournamentAsync(id, tournamentDto);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] TournamentStatus status)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateStatusAsync(id, status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if (id < 1) return BadRequest();
            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
