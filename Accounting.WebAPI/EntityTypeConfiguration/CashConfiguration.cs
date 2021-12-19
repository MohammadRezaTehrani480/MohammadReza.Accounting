using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.WebAPI.EntityTypeConfiguration
{
    public class CashConfiguration : IEntityTypeConfiguration<Cash>
    {
        public void Configure(EntityTypeBuilder<Cash> builder)
        {
            builder.HasMany(current => current.Documents)
                    .WithOne(doc => doc.Cash)
                    .HasForeignKey(f => f.CashId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Property(l => l.CashName)
                    .HasMaxLength(20);
        }
    }
}
