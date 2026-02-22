using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TeamInvite:BaseEntity
    {
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public InviteStatus Status { get; set; }
        public DateTime SentAt { get; set; }
    }
}
