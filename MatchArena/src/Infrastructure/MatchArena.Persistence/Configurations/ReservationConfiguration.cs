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
    internal class ReservationConfiguration:IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReservedDate)
                   .IsRequired();

            builder.Property(x => x.ReservedTime)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Field)
                   .WithMany()
                   .HasForeignKey(x => x.FieldId);

            builder.HasOne(x => x.Payment)
                   .WithMany()
                   .HasForeignKey(x => x.PaymentId);


            builder.HasIndex(x => new { x.FieldId, x.ReservedDate, x.ReservedTime })
                   .IsUnique();
        }
    }
}
