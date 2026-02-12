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
    internal class FieldImageConfiguration:IEntityTypeConfiguration<FieldImage>
    {
        public void Configure(EntityTypeBuilder<FieldImage> builder)
        {
            builder.Property(fi => fi.Image)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(fi => fi.FieldId)
                .IsRequired();
 
            builder.HasOne(fi => fi.Field)
                .WithMany(f => f.Images)
                .HasForeignKey(fi => fi.FieldId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(fi => fi.FieldId);
        }
    }
}

