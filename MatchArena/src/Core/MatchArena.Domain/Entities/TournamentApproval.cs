using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TournamentApproval:BaseAccountableEntity
    {
        public long TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public bool IsApproved { get; set; }
        public string AdminNote { get; set; }
    }
}
