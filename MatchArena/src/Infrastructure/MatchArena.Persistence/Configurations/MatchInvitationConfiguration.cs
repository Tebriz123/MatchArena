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
    internal class MatchInvitationConfiguration : IEntityTypeConfiguration<MatchInvitation>
    {
        public void Configure(EntityTypeBuilder<MatchInvitation> builder)
        {
            builder.Property(i => i.CreatedAt)
               .IsRequired();

            builder.HasOne<Team>()
                   .WithMany()
                   .HasForeignKey(i => i.FromTeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                   .WithMany()
                   .HasForeignKey(i => i.ToTeamId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
