using MatchArena.Application.DTOs.Fields;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _service;

        public FieldsController(IFieldService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync(int take = 0, int page = 0)
        {
            return Ok(await _service.GetAllAsync(take, page));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            if (id < 1) return BadRequest();
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostFieldDto fieldDto)
        {
            await _service.CreateFieldAsync(fieldDto);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(long id, [FromBody] PutFieldDto fieldDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateFieldAsync(id, fieldDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if(id < 1) return BadRequest(); 
            await _service.Remove(id);
            return NoContent();
        }
    }
}
