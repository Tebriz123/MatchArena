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

    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
           
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Logo)
                .HasMaxLength(500);

            builder.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.PlayerCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.MaxPlayer)
                .IsRequired()
                .HasDefaultValue(25);

            builder.Property(t => t.Rating)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.GameCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.Information)
                .HasMaxLength(1000);

            builder.HasMany(t => t.TeamPlayers)
                .WithOne(tp => tp.Team)
                .HasForeignKey(tp => tp.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
