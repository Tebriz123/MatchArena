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
    internal class TournamentRegistrationConfiguration: IEntityTypeConfiguration<TournamentRegistration>
    {
        public void Configure(EntityTypeBuilder<TournamentRegistration> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.HasOne(x => x.Tournament)
                   .WithMany()
                   .HasForeignKey(x => x.TournamentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Team)
                   .WithMany()
                   .HasForeignKey(x => x.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Captain)
                   .WithMany()
                   .HasForeignKey(x => x.CaptainUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Payment)
                   .WithMany()
                   .HasForeignKey(x => x.PaymentId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.TournamentId, x.TeamId })
                   .IsUnique();
        }
    }
}
