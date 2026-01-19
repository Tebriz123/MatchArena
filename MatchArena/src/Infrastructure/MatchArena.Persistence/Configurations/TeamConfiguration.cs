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
               .HasMaxLength(100);

            builder.HasOne(t => t.Captain)
                   .WithMany()
                   .HasForeignKey(t => t.CaptainId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
