using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Tournament:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }

        //Tournament Parameters
        public int MaxTeams { get; set; }
        public int CurrentTeams { get; set; }
        public decimal EntryFee { get; set; }
        public decimal PrizeFund { get; set; }
        public string Format { get; set; } = string.Empty;
        public int GameDuration { get; set; }
        public string GameFormat { get; set; }=string.Empty;
        public decimal Rating { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public TournamentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<Field> Fields { get; set; }
    }
}
