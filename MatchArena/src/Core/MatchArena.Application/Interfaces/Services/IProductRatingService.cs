using MatchArena.Application.DTOs.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IProductRatingService
    {
        Task PostProductRatingAsync(string userId, long productId, PostRatingDto dto);
        Task<GetProductRatingResponseDto> GetProductRatingsAsync(long productId);

    }
}
