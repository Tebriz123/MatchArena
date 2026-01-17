using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TournamentTeam:BaseEntity
    {
        public long TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; }
    }
}
