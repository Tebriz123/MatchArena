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
    public class PlayerRatingService : IPlayerRatingService
    {
        private readonly IPlayerRatingRepository _playerRatingRepository;
        private readonly IMapper _mapper;

        public PlayerRatingService(IPlayerRatingRepository playerRatingRepository, IMapper mapper)
        {
            _playerRatingRepository = playerRatingRepository;
            _mapper = mapper;
        }

        public async Task PostPlayerRatingAsync(long raterPlayerId, long ratedPlayerId, PostRatingDto dto)
        {
            if (raterPlayerId == ratedPlayerId)
                throw new Exception("You cannot rate yourself.");

            bool alreadyRated = await _playerRatingRepository
                .AnyAsync(pr => pr.RaterPlayerId == raterPlayerId && pr.RatedPlayerId == ratedPlayerId);

            if (alreadyRated)
                throw new Exception("You have already rated this player.");

            var playerRating = _mapper.Map<PlayerRating>(dto);
            playerRating.RaterPlayerId = raterPlayerId;
            playerRating.RatedPlayerId = ratedPlayerId;
            playerRating.RatedAt = DateTime.UtcNow;

            _playerRatingRepository.Add(playerRating);
            await _playerRatingRepository.SaveChangesAsync();
        }

        public async Task<GetPlayerRatingResponseDto> GetPlayerRatingsAsync(long playerId)
        {
            var ratings = await _playerRatingRepository
                .GetAll(pr => pr.RatedPlayerId == playerId, includes: nameof(PlayerRating.RaterPlayer))
                .ToListAsync();

            return new GetPlayerRatingResponseDto
            {
                TotalRatings = ratings.Count,
                AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0,
                Ratings = _mapper.Map<List<GetRatingItemDto>>(ratings)
            };
        }
    }
}
