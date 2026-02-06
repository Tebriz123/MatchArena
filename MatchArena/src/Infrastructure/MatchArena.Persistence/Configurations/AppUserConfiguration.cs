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
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(x => x.Surname)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(u => u.Player)
              .WithOne(p => p.User)
              .HasForeignKey<Player>(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
