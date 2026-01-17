using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TournamentMatch:BaseEntity
    {
        public long TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public long TeamAId { get; set; }
        public Team TeamA { get; set; }

        public long TeamBId { get; set; }
        public Team TeamB { get; set; }

        public int? ScoreA { get; set; }
        public int? ScoreB { get; set; }
    }
}
