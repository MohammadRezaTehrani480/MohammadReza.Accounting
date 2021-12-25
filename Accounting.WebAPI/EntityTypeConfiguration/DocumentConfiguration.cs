using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.WebAPI.EntityTypeConfiguration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(x => x.Amount)
                   .IsRequired();

            builder.Property(x => x.Date)
                    .IsRequired();

            builder.Property(x => x.DocNo)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(x => x.DocTypeId)
                    .IsRequired();

            builder.Property(x => x.PersonId)
                    .IsRequired();

            builder.Property(x => x.CashId)
                    .IsRequired();
        }
    }
}
