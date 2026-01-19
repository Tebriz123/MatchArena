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
    internal class FieldScheduleConfiguration : IEntityTypeConfiguration<FieldSchedule>
    {
        public void Configure(EntityTypeBuilder<FieldSchedule> builder)
        {
            builder.HasOne<Field>()
               .WithMany(f => f.Schedules)
               .HasForeignKey(s => s.FieldId);

            builder.Property(s => s.IsReserved)
                   .HasDefaultValue(false);

        }
    }
}
