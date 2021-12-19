using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.WebAPI.EntityTypeConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Address)
                    .HasMaxLength(200)
                    .IsUnicode()
                    .IsRequired();

            //builder.HasDiscriminator(p => p.Discriminator)
            //        .HasValue<RealPerson>(nameof(RealPerson))
            //        .HasValue<LegalPerson>(nameof(LegalPerson));

            //builder.Property(person => person.Discriminator)
            //        .HasColumnName("Person_Type")
            //        .HasMaxLength(12);

            builder.Property(p => p.PhoneNumber)
                    .HasMaxLength(11)
                    .IsUnicode()
                    .IsRequired();

            builder.Property(p => p.Email)
                    .HasMaxLength(200)
                    .IsUnicode()
                    .IsRequired();

            builder.HasMany(current => current.Documents)
                    .WithOne(d => d.AccountSide)
                    .HasForeignKey(f => f.PersonId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
