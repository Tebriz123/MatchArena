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
    internal class PlayerRatingConfiguration : IEntityTypeConfiguration<PlayerRating>
    {
        public void Configure(EntityTypeBuilder<PlayerRating> builder)
        {
            builder.Property(r => r.Rating)
               .IsRequired();

            builder.HasOne<PlayerProfile>()
                   .WithMany()
                   .HasForeignKey(r => r.FromPlayerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<PlayerProfile>()
                   .WithMany()
                   .HasForeignKey(r => r.ToPlayerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
