using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class SystemStatistic:BaseEntity
    {
        public int TotalUsers { get; set; }
        public int TotalTeams { get; set; }
        public int TotalMatches { get; set; }
        public int TotalFields { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
