using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Field:BaseNameableEntity
    {
        public string Address { get; set; }
        public string City { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Image { get; set; }
        public ICollection<FieldImage> Images { get; set; }
        public ICollection<DateTime> EmptySpace { get; set; }

        public double AverageRating => FieldRatings != null && FieldRatings.Any() ?
            FieldRatings.Average(r => r.Rating) : 0;
        public int TotalRating => FieldRatings?.Count ?? 0;
        public ICollection<FieldRating> FieldRatings { get; set; }
    }
}
