using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Team:BaseNameableEntity
    {

        public long CaptainId { get; set; }
        public PlayerProfile Captain { get; set; }
        public ICollection<TeamPlayer> Players { get; set; }

    }
}
