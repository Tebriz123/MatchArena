using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Configurations
{
    internal class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {

            builder.Property(f => f.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(f => f.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.PricePerHour)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasMany(f => f.Images)
                .WithOne(fi => fi.Field)
                .HasForeignKey(fi => fi.FieldId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(f => f.EmptySpace)
         .HasConversion(
             v => JsonSerializer.Serialize(v.Select(t => t.ToString("HH:mm")).ToList(), (JsonSerializerOptions)null),
             v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                 .Select(s => TimeOnly.Parse(s))
                 .ToList()
         )
         .HasColumnType("nvarchar(max)");

        }
    }
}
