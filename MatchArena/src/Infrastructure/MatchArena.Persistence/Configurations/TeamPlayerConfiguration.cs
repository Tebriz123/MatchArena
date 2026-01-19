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
    internal class TeamPlayerConfiguration : IEntityTypeConfiguration<TeamPlayer>
    {
        public void Configure(EntityTypeBuilder<TeamPlayer> builder)
        {
            builder.HasKey(tp => new { tp.TeamId, tp.PlayerProfileId });

            builder.HasOne(tp => tp.Team)
                   .WithMany(t => t.Players)
                   .HasForeignKey(tp => tp.TeamId);

            builder.HasOne(tp => tp.PlayerProfile)
                   .WithMany()
                   .HasForeignKey(tp => tp.PlayerProfileId);
        }
    }
}
