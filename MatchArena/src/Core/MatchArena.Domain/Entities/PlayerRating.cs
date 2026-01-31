using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class PlayerRating:BaseAccountableEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        //reletion

        public long FromPlayerId { get; set; }
        public Player FromPlayer { get; set; }
        public long ToPlayerId { get; set; }
        public Player ToPlayer { get; set; }

    }
}
