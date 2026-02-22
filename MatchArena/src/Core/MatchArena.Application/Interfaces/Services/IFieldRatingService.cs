using MatchArena.Application.DTOs.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IFieldRatingService
    {
        Task PostFieldRatingAsync(long playerId, long fieldId, PostRatingDto dto);
        Task<GetFieldRatingResponseDto> GetFieldRatingsAsync(long fieldId);
    }
}
