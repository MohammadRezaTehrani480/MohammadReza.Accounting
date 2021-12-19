using Accounting.WebAPI.Entities;
using Accounting.WebAPI.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Data
{
    public static class AccountingContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lookup>().HasData(
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
                }
                );

            modelBuilder.Entity<Lookup>().HasData(
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
                }
                );

            modelBuilder.Entity<Lookup>().HasData(
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

            modelBuilder.Entity<RealPerson>().HasData(
               new RealPerson
               {
                   Id = 20,
                   FirstName = "Mohammad Reza",
                   LastName = "Tehrani",
                   FatherName = "Ali",
                   PhoneNumber = "09177973283",
                   Email = "tehtanimohmmad480@gmail.com",
                   Address = "Karaj",
                   NationalCode = "0440799996"
               });

            modelBuilder.Entity<Cash>().HasData(
               new Cash
               {
                   Id = 1,
                   CashName = "Cash 1",
                   RealPersonId = 1
               });
        }
    }
}
