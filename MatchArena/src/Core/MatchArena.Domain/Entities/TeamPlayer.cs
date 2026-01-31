using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TeamPlayer:BaseEntity
    {
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long PlayerProfileId { get; set; }
        public Player PlayerProfile { get; set; }
    }
}
