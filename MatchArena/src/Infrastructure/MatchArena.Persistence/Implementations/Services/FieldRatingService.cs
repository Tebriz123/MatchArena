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
    public class FieldRatingService : IFieldRatingService
    {
        private readonly IFieldRatingRepository _fieldRatingRepository;
        private readonly IMapper _mapper;

        public FieldRatingService(IFieldRatingRepository fieldRatingRepository, IMapper mapper)
        {
            _fieldRatingRepository = fieldRatingRepository;
            _mapper = mapper;
        }

        public async Task PostFieldRatingAsync(long playerId, long fieldId, PostRatingDto dto)
        {
            bool alreadyRated = await _fieldRatingRepository
                .AnyAsync(fr => fr.PlayerId == playerId && fr.FieldId == fieldId);

            if (alreadyRated)
                throw new Exception("You have already rated this field.");

            var fieldRating = _mapper.Map<FieldRating>(dto);
            fieldRating.PlayerId = playerId;
            fieldRating.FieldId = fieldId;
            fieldRating.RatedAt = DateTime.UtcNow;

            _fieldRatingRepository.Add(fieldRating);
            await _fieldRatingRepository.SaveChangesAsync();
        }

        public async Task<GetFieldRatingResponseDto> GetFieldRatingsAsync(long fieldId)
        {
            var ratings = await _fieldRatingRepository
                .GetAll(fr => fr.FieldId == fieldId, includes: nameof(Player))
                .ToListAsync();

            return new GetFieldRatingResponseDto
            {
                TotalRatings = ratings.Count,
                AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0,
                Ratings = _mapper.Map<List<GetRatingItemDto>>(ratings)
            };
        }
    }
}
