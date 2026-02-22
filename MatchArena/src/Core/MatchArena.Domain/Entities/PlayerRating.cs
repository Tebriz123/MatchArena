using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class PlayerRating:BaseEntity
    {
        public DateTime RatedAt { get; set; } = DateTime.UtcNow;
        public string? Comment { get; set; }
        public int Rating { get; set; }

        public long RaterPlayerId { get; set; }
        public Player RaterPlayer { get; set; }

        public long RatedPlayerId { get; set; }
        public Player RatedPlayer { get; set; }

    }
}
