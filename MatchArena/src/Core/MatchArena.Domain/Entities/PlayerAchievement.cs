using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class PlayerAchievement:BaseEntity
    {
        public DateTime EarnedAt { get; set; }

        //reletion

        public long PlayerProfileId { get; set; }
        public PlayerProfile Player { get; set; }

        public long AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
