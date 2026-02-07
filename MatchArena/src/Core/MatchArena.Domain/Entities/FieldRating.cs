using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class FieldRating:BaseEntity
    {
        public DateTime RatedAt { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }

        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public long FieldId { get; set; }
        public Field Field { get; set; }
    }
}
