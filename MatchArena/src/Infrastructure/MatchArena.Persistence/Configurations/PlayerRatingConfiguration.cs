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
            builder.HasOne(pr => pr.RaterPlayer)
                   .WithMany(p => p.GivenRatings)
                   .HasForeignKey(pr => pr.RaterPlayerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pr => pr.RatedPlayer)
                   .WithMany(p => p.ReceivedRatings)
                   .HasForeignKey(pr => pr.RatedPlayerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
