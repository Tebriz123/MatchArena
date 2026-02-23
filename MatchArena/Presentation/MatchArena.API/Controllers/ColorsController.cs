using MatchArena.Application.DTOs.Colors;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorRepository _repository;
        private readonly IColorService _service;

        public ColorsController(IColorRepository repository, IColorService service)
        {
            _repository = repository;
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
            await _service.CreateAsync(new PostColorDto(Name));

            return Created();

        }
        [HttpPut("{id}")]
        [Authorize]
        [Area("Admin")]

        public async Task<IActionResult> Update(int id, [FromForm] PutColorDto colorDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(colorDto, id);

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
