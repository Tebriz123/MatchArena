using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{

    public class PlayerProfile : BaseAccountableEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int Age { get; set; }
        public PlayerPosition Position { get; set; }
        public PlayerLevel Level { get; set; }

        public string City { get; set; }
        public double Rating { get; set; }
        public int PlayedMatches { get; set; }
        public ICollection<PlayerRating> RatingsReceived { get; set; }
    }





}
