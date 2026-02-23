using MatchArena.Application.DTOs.Sizes;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _service;

        public SizesController(ISizeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();


            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> Create([FromForm] string Name)
        {
            await _service.CreateAsync(new PostSizeDto(Name));

            return Created();

        }
        [HttpPut("{id}")]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> Update(int id, [FromForm] PutSizeDto sizeDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(sizeDto, id);

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize]
        [Area("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
