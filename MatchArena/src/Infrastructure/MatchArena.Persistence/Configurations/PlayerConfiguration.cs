

using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchArena.Persistence.Configurations
{

    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(p => p.Age)
                .IsRequired();

            builder.Property(p => p.Position)
                .IsRequired();

            builder.Property(p => p.Level)
                .IsRequired();

            builder.Property(p => p.IsCapitain)
                .IsRequired();

            builder.Property(p => p.Image)
                .HasMaxLength(500);

            builder.Property(p => p.City)
                .HasMaxLength(100);

            builder.Property(p => p.Rating)
                .IsRequired();

            builder.Property(p => p.PlayedMatches)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PlayerTeams)
                .WithOne(pt => pt.Player)
                .HasForeignKey(pt => pt.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.UserId);

        }
    }
}
