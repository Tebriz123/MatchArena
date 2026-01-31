

using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchArena.Persistence.Configurations
{

    internal class PlayeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {

            builder.Property(p => p.City)
                   .HasMaxLength(50);

            builder.Property(p => p.Rating)
                   .HasDefaultValue(0);

            builder.Property(p => p.PlayedMatches)
                   .HasDefaultValue(0);

            builder.Property(p => p.Position)
                   .IsRequired();

            builder.Property(p => p.Level)
                   .IsRequired();
        }
    }
}
