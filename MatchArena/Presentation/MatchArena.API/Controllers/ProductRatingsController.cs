using MatchArena.Application.DTOs.Ratings;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductRatingsController : ControllerBase
    {
        private readonly IProductRatingService _service;

        public ProductRatingsController(IProductRatingService service)
        {
            _service = service;
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetRatings(long productId)
        {
            var result = await _service.GetProductRatingsAsync(productId);
            return Ok(result);
        }

        [HttpPost("{productId}")]
        [Authorize]
        public async Task<IActionResult> PostRating(long productId, PostRatingDto ratingDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated.");

            await _service.PostProductRatingAsync(userId, productId, ratingDto);
            return Created();
        }
    }
}
