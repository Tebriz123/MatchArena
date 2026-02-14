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
        public string FieldInformation { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Image { get; set; }
        public ICollection<FieldImage> Images { get; set; }
        public ICollection<TimeOnly> EmptySpace { get; set; } = new List<TimeOnly>();

        public double AverageRating => FieldRatings != null && FieldRatings.Any() ?
            FieldRatings.Average(r => r.Rating) : 0;
        public int TotalRating => FieldRatings?.Count ?? 0;
        public ICollection<FieldRating> FieldRatings { get; set; } = new List<FieldRating>();
    }
}
