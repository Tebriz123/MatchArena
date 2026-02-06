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
    internal class TournamentConfiguration:IEntityTypeConfiguration<Tournament>
    {

        public void Configure(EntityTypeBuilder<Tournament> builder)
        {

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(t => t.Logo)
                .HasMaxLength(500);

            builder.Property(t => t.MaxTeams)
                .IsRequired();

            builder.Property(t => t.CurrentTeams)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.EntryFee)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(t => t.PrizeFund)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(t => t.Format)
                .HasMaxLength(100);

            builder.Property(t => t.GameDuration)
                .IsRequired();

            builder.Property(t => t.GameFormat)
                .HasMaxLength(100);

            builder.Property(t => t.Raiting)
                .HasColumnType("decimal(3,2)")
                .IsRequired();

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired();

            builder.Property(t => t.RegistrationDeadline)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(t => t.Teams)
                .WithMany()
                .UsingEntity(j => j.ToTable("TournamentTeams"));

            builder.HasIndex(t => t.Status);
            builder.HasIndex(t => t.StartDate);
        }
    }
}
