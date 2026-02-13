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
            builder.HasKey(tp => new { tp.TeamId, tp.PlayerId });

            builder.Property(tp => tp.IsCaptain)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(tp => tp.TeamId)
                .IsRequired();

            builder.Property(tp => tp.PlayerId)
                .IsRequired();

            builder.HasOne(tp => tp.Team)
                .WithMany(t => t.TeamPlayers)
                .HasForeignKey(tp => tp.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tp => tp.Player)
                .WithMany(p => p.PlayerTeams)
                .HasForeignKey(tp => tp.PlayerId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasIndex(tp => tp.TeamId);
            builder.HasIndex(tp => tp.PlayerId);

            builder.HasIndex(tp => new { tp.TeamId, tp.IsCaptain })
                .IsUnique()
                .HasFilter("[IsCaptain] = 1"); 
        }

    }
}
