using MatchArena.Application.DTOs.Ratings;
using MatchArena.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerRatingsController : ControllerBase
    {
        private readonly IPlayerRatingService _service;

        public PlayerRatingsController(IPlayerRatingService service)
        {
            _service = service;
        }
        [HttpGet("{playerId}")]

        public async Task<IActionResult> GetRatings(long playerId)
        {
            var result = await _service.GetPlayerRatingsAsync(playerId);
            return Ok(result);
        }

        [HttpPost("{ratedPlayerId}")]
        [Authorize]
        public async Task<IActionResult> PostRating(long raterPlayerId, long ratedPlayerId, PostRatingDto ratingDto)
        {
            await _service.PostPlayerRatingAsync(raterPlayerId, ratedPlayerId, ratingDto);
            return Created();
        } 
    }
}
