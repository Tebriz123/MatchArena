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
    internal class TeamInviteConfiguration : IEntityTypeConfiguration<TeamInvite>
    {
        public void Configure(EntityTypeBuilder<TeamInvite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.SentAt)
                   .IsRequired();

            builder.HasOne(x => x.Team)
                   .WithMany()
                   .HasForeignKey(x => x.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Player)
                   .WithMany()
                   .HasForeignKey(x => x.PlayerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.TeamId, x.PlayerId })
                   .IsUnique();

        }
    }
}
