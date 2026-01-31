using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TeamReport:BaseAccountableEntity
    {

        public string Reason { get; set; }
        public bool IsResolved { get; set; }
        
        public long TeamId { get; set; }
        public Team Team { get; set; }

        public long ReportedByPlayerId { get; set; }
        public Player ReportedByPlayer { get; set; }

       
    }
}
