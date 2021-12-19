using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.EntityTypeConfiguration
{
    public class LegalPersonConfiguration : IEntityTypeConfiguration<LegalPerson>
    {
        public void Configure(EntityTypeBuilder<LegalPerson> builder)
        {
            builder.Property(l => l.EconomicCode)
                    .HasMaxLength(4);

            builder.Property(l => l.RegistrationCode)
                    .HasMaxLength(4);

            builder.Property(l => l.CompanyNo)
                    .HasMaxLength(4)
                    .IsRequired();
        }
    }
}
