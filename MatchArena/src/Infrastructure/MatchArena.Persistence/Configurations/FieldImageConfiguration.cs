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
    internal class FieldImageConfiguration : IEntityTypeConfiguration<FieldImage>
    {
        public void Configure(EntityTypeBuilder<FieldImage> builder)
        {
            builder.Property(i => i.ImageUrl)
               .IsRequired();

            builder.HasOne<Field>()
                   .WithMany(f => f.Images)
                   .HasForeignKey(i => i.FieldId);
        }
    }
}
