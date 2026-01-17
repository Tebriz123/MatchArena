using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Tournament:BaseNameableEntity
    {
        public DateTime StartDate { get; set; }
        public string CreatedByUserId { get; set; }

        public ICollection<TournamentTeam> Teams { get; set; }
    }
}
