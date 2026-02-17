using MatchArena.Application.DTOs.Sizes;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeRepository _repository;
        private readonly ISizeService _service;

        public SizesController(ISizeRepository repository, ISizeService service)
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
        public async Task<IActionResult> Create([FromForm] PostSizeDto sizeDto)
        {
            await _service.CreateAsync(sizeDto);

            return Created();

        }
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] PutSizeDto sizeDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(sizeDto, id);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
