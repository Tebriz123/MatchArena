using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.DTOs.Ratings
{
    public class GetRatingItemDto
    {
        public string UserName { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime RatedAt { get; set; }
    }
}
