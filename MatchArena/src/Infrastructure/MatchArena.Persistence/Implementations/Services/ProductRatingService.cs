using AutoMapper;
using MatchArena.Application.DTOs.Ratings;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    public class ProductRatingService : IProductRatingService
    {
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IMapper _mapper;

        public ProductRatingService(IProductRatingRepository productRatingRepository, IMapper mapper)
        {
            _productRatingRepository = productRatingRepository;
            _mapper = mapper;
        }

        public async Task PostProductRatingAsync(string userId, long productId, PostRatingDto dto)
        {
            bool alreadyRated = await _productRatingRepository
                .AnyAsync(pr => pr.UserId == userId && pr.ProductId == productId);

            if (alreadyRated)
                throw new Exception("You have already rated this product.");

            var productRating = _mapper.Map<ProductRating>(dto);
            productRating.UserId = userId;
            productRating.ProductId = productId;
            productRating.RatedAt = DateTime.UtcNow;

            _productRatingRepository.Add(productRating);
            await _productRatingRepository.SaveChangesAsync();
        }

        public async Task<GetProductRatingResponseDto> GetProductRatingsAsync(long productId)
        {
            var ratings = await _productRatingRepository
                     .GetAll(pr => pr.ProductId == productId, includes: "User")
                     .ToListAsync();

            return new GetProductRatingResponseDto
            {
                TotalRatings = ratings.Count,
                AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0,
                Ratings = _mapper.Map<List<GetRatingItemDto>>(ratings)
            };
        }
    }
}
