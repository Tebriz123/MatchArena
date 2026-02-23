using MatchArena.Application.DTOs.Categories;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
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
        [Authorize]
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            await _service.CreateAsync(new PostCategoryDto(name));

            return Created();

        }

        [Authorize]
        [Area("Admin")]

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] PutCategoryDto categoryDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(categoryDto, id);

            return NoContent();
        }

        [Authorize]
        [Area("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
