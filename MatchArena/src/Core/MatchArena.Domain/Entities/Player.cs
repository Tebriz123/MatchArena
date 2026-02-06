using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{

    public class Player : BaseNameableEntity    
    {
        public string Surname { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int Age { get; set; }
        public PlayerPosition Position { get; set; }
        public PlayerLevel Level { get; set; }
        public bool IsCapitain { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public double Rating { get; set; }
        public int PlayedMatches { get; set; }
        public ICollection<TeamPlayer> PlayerTeams { get; set; }
    }





}
