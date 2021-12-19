using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Accounting.WebAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Accounting.WebAPI.Enum;

namespace Accounting.WebAPI.EntityTypeConfiguration
{
    public class LookupConfiguraton : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.Property(l => l.Title)
                    .IsRequired();

            builder.Property(x => x.LookupTypeId)
                    .IsRequired();

            builder.HasMany(current => current.Documents)
                    .WithOne(d => d.DocType)
                    .HasForeignKey(f => f.DocTypeId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(current => current.RealPeople)
                   .WithOne(d => d.BirthPlace)
                   .HasForeignKey(f => f.BirthPlaceId)
                   .HasForeignKey(f => f.NationalityId)
                   .OnDelete(DeleteBehavior.NoAction);



            //=======================================================

            /*Lookup data seed*/

            builder.HasData(
               new Lookup
               {
                   Id = 1,
                   Title = "withdrawal",
                   LookupTypeId = (int)LookupType.Documents
               },
               new Lookup
               {
                   Id = 2,
                   Title = "Payment",
                   LookupTypeId = (int)LookupType.Documents
               },
               new Lookup
               {
                   Id = 3,
                   Title = "Iranian",
                   LookupTypeId = (int)LookupType.Nationalities
               },
               new Lookup
               {
                   Id = 4,
                   Title = "Foreigner",
                   LookupTypeId = (int)LookupType.Nationalities
               },
               new Lookup
               {
                   Id = 5,
                   Title = "Tehran",
                   LookupTypeId = (int)LookupType.Cities
               },
               new Lookup
               {
                   Id = 6,
                   Title = "Karaj",
                   LookupTypeId = (int)LookupType.Cities
               }
               ,
               new Lookup
               {
                   Id = 7,
                   Title = "Shiraz",
                   LookupTypeId = (int)LookupType.Cities
               },
               new Lookup
               {
                   Id = 8,
                   Title = "Gilan",
                   LookupTypeId = (int)LookupType.Cities
               }
               );
        }
    }
}
