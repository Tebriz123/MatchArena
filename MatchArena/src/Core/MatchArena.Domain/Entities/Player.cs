using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{

    public class Player : BaseAccountableEntity    
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int Age { get; set; }
        public PlayerPosition Position { get; set; }
        public PlayerLevel Level { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public int Rating { get; set; }
        public int PlayedMatches { get; set; }
        public int Height { get; set; }
        public int GameCount { get; set; }
        public string Information { get; set; }
        public int Goal { get; set; }
        public ICollection<TeamPlayer> PlayerTeams { get; set; }
    }





}
