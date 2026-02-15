using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TeamPlayer:BaseEntity
    {
        public bool IsCaptain { get; set; }
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long? PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
