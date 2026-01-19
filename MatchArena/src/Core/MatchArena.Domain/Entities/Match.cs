using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Match:BaseEntity
    {
        public DateTime MatchDate { get; set; }

        //reletion


        public long TeamAId { get; set; }
        public Team TeamA { get; set; }
        public long TeamBId { get; set; }
        public Team TeamB { get; set; }
        public long? FieldId { get; set; }
        public Field Field { get; set; }
        public MatchStatus Status { get; set; }

    }
}
