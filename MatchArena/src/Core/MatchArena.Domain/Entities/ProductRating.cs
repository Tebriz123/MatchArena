using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class ProductRating:BaseEntity
    {
        public DateTime RatedAt { get; set; } = DateTime.UtcNow;
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
