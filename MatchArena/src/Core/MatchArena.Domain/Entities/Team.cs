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
        public Player Captain { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public string Logo { get; set; }

    }
}
