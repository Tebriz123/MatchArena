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
        public Player Player { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public string Logo { get; set; }
        public string City { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayer { get; set; }
        public double Rating { get; set; }
        public int GameCount { get; set; }
        public string Information { get; set; }
    }
}
