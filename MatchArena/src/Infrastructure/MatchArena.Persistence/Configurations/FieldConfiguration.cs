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
    internal class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(f => f.Address)
                   .IsRequired();

            builder.Property(f => f.PricePerHour)
                   .HasColumnType("decimal(10,2)");
        }
    }
}
