using MatchArena.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Configurations
{
    internal class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Color> builder)
        {
            builder
               .Property(c => c.Name)
               .IsRequired()
               .HasColumnType("varchar(50)");
            builder
                .HasIndex(c => c.Name)
                    .IsUnique();
        }
    }
}
