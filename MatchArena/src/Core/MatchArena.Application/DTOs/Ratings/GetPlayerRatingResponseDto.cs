using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Ratings
{
    public class GetPlayerRatingResponseDto
    {
        public int TotalRatings { get; set; }
        public double AverageRating { get; set; }
        public List<GetRatingItemDto> Ratings { get; set; }
    }
}
