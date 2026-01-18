using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class PlayerReport:BaseAccountableEntity
    {
        public string Reason { get; set; }
        public bool IsResolved { get; set; }


        public long ReportedPlayerId { get; set; }
        public PlayerProfile ReportedPlayer { get; set; }

        public long ReportedByPlayerId { get; set; }
        public PlayerProfile ReportedByPlayer { get; set; }

    
    }
}
