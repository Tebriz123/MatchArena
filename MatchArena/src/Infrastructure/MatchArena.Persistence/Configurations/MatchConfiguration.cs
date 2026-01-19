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
    internal class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(m => m.TeamA)
                .WithMany()
                .HasForeignKey(m => m.TeamAId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.TeamB)
                   .WithMany()
                   .HasForeignKey(m => m.TeamBId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Field)
                   .WithMany()
                   .HasForeignKey(m => m.FieldId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(m => m.Status)
                   .IsRequired();
        }
    }
}
