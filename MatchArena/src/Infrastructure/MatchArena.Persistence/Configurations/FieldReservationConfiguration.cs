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
    internal class FieldReservationConfiguration : IEntityTypeConfiguration<FieldReservation>
    {
        public void Configure(EntityTypeBuilder<FieldReservation> builder)
        {
            builder.Property(r => r.ReservedAt)
               .IsRequired();
        }
    }
}
