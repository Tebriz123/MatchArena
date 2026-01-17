using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class MatchInvitation:BaseAccountableEntity
    {
        public bool IsAccepted { get; set; }

        //reletion
        public long FromTeamId { get; set; }
        public Team FromTeam { get; set; }

        public long ToTeamId { get; set; }
        public Team ToTeam { get; set; }

        
    }
}
