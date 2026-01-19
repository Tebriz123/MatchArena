using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Configurations
{
    internal class PlayerAchievementConfiguration : IEntityTypeConfiguration<PlayerAchievement>
    {
        public void Configure(EntityTypeBuilder<PlayerAchievement> builder)
        {
            builder.HasKey(pa => new { pa.PlayerProfileId, pa.AchievementId });

        }
    }
}
