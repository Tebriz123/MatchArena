using MatchArena.Application.DTOs.Ratings;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FieldRatingsController : ControllerBase
    {
        private readonly IFieldRatingService _service;

        public FieldRatingsController(IFieldRatingService service)
        {
            _service = service;
        }
        [HttpGet("{fieldId}")]
        [Authorize]
        public async Task<IActionResult> GetRatings(long fieldId)
        {
            var result = await _service.GetFieldRatingsAsync(fieldId);
            return Ok(result);
        }

        [HttpPost("{fieldId}")]
        [Authorize]
        public async Task<IActionResult> PostRating(long playerId, long fieldId, PostRatingDto ratingDto)
        {
            await _service.PostFieldRatingAsync(playerId, fieldId, ratingDto);
            return Created();
        }
    }
}
