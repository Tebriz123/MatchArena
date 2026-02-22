using MatchArena.Application.DTOs.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IPlayerRatingService
    {
        Task PostPlayerRatingAsync(long raterPlayerId, long ratedPlayerId, PostRatingDto dto);
        Task<GetPlayerRatingResponseDto> GetPlayerRatingsAsync(long playerId);
    }
}
