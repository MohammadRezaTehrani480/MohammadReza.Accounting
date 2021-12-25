using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;
using Accounting.WebAPI.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.WebAPI.EntityTypeConfiguration
{
    public class RealPersonConfiguration : IEntityTypeConfiguration<RealPerson>
    {
        public void Configure(EntityTypeBuilder<RealPerson> builder)
        {
            builder.Property(x => x.NationalCode)
                    .IsRequired()
                    .HasMaxLength(10);

            builder.Property(x => x.FirstName)
                     .IsRequired()
                     .HasMaxLength(50);

            builder.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(x => x.FatherName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(x => x.BirthPlaceId)
                   .IsRequired();

            builder.Property(x => x.Age)
                   .IsRequired();

            builder.HasMany(r => r.Cashes)
                    .WithOne(c => c.RealPerson)
                    .HasForeignKey(x => x.RealPersonId)
                    .OnDelete(DeleteBehavior.Cascade);

            //=====================================================

            /*RealPerson Data Seed*/

            builder.HasData(
                new RealPerson
                {
                    Id = 1,
                    Address = "Karaj",
                    PhoneNumber = "09177973283",
                    Email = "tehranimohammad480",
                    NationalCode = "0440799996",
                    FirstName = "Mohammad Reza",
                    LastName = "Tehrani",
                    BirthPlaceId = 6,
                    BirthDate = new DateTime(1997, 03, 14),
                    FatherName = "Ali",
                    NationalityId = 3,
                    Age = 10
                });

            builder.HasData(
                new RealPerson
                {
                    Id = 2,
                    Address = "Tehran",
                    PhoneNumber = "09171619993",
                    Email = "tehraniAli480",
                    NationalCode = "1234567890",
                    FirstName = "Ali",
                    LastName = "Tehrani",
                    BirthPlaceId = 5,
                    BirthDate = new DateTime(1985, 05, 12),
                    FatherName = "Kamal",
                    NationalityId = 3,
                    Age = 20
                });
            builder.HasData(
                new RealPerson
                {
                    Id = 3,
                    Address = "Abadan",
                    PhoneNumber = "09174856699",
                    Email = "sdlksdkvnksnv",
                    NationalCode = "0440799996",
                    FirstName = "Reza",
                    LastName = "Bogari",
                    BirthPlaceId = 5,
                    BirthDate = new DateTime(1997, 03, 14),
                    FatherName = "Ali",
                    NationalityId = 3,
                    Age = 15
                });

            builder.HasData(
                new RealPerson
                {
                    Id = 4,
                    Address = "Tehran",
                    PhoneNumber = "01478747879",
                    Email = "tehraniAli480",
                    NationalCode = "2546845865",
                    FirstName = "Mahyar",
                    LastName = "Bogari",
                    BirthPlaceId = 6,
                    BirthDate = new DateTime(1985, 05, 12),
                    FatherName = "Kamal",
                    NationalityId = 3,
                    Age = 47
                });
            builder.HasData(
                new RealPerson
                {
                    Id = 5,
                    Address = "Karaj",
                    PhoneNumber = "01478954789",
                    Email = "tehranimohammad480",
                    NationalCode = "147569874",
                    FirstName = "Mohammad Reza",
                    LastName = "Tehrani",
                    BirthPlaceId = 7,
                    BirthDate = new DateTime(1997, 03, 14),
                    FatherName = "Ali",
                    NationalityId = 3,
                    Age = 14
                });

            builder.HasData(
                new RealPerson
                {
                    Id = 6,
                    Address = "Tehran",
                    PhoneNumber = "01236987474",
                    Email = "rggrg",
                    NationalCode = "9898989745",
                    FirstName = "Ali",
                    LastName = "Tehrani",
                    BirthPlaceId = 8,
                    BirthDate = new DateTime(1985, 05, 12),
                    FatherName = "Kamal",
                    NationalityId = 3,
                    Age = 12
                });
            builder.HasData(
                new RealPerson
                {
                    Id = 7,
                    Address = "Karaj",
                    PhoneNumber = "01478745454",
                    Email = "tehranimohammad480",
                    NationalCode = "4565654568",
                    FirstName = "Ahmad Reza",
                    LastName = "Tehrani",
                    BirthPlaceId = 5,
                    BirthDate = new DateTime(1997, 03, 14),
                    FatherName = "Ali",
                    NationalityId = 3,
                    Age = 45
                });

            builder.HasData(
                new RealPerson
                {
                    Id = 8,
                    Address = "Tehran",
                    PhoneNumber = "09171619993",
                    Email = "tehraniAli480",
                    NationalCode = "1234567890",
                    FirstName = "Ali",
                    LastName = "Tehrani",
                    BirthPlaceId = 6,
                    BirthDate = new DateTime(1985, 05, 12),
                    FatherName = "Kamal",
                    NationalityId = 3,
                    Age = 20
                });
            builder.HasData(
                new RealPerson
                {
                    Id = 9,
                    Address = "Karaj",
                    PhoneNumber = "01478954758",
                    Email = "djjfgodf",
                    NationalCode = "1578947524",
                    FirstName = "Mohammad Ali",
                    LastName = "Tehrani",
                    BirthPlaceId = 7,
                    BirthDate = new DateTime(1788, 03, 14),
                    FatherName = "Parsa",
                    NationalityId = 4,
                    Age = 25
                });

            builder.HasData(
                new RealPerson
                {
                    Id = 10,
                    Address = "Tehran",
                    PhoneNumber = "09171619993",
                    Email = "tehraniAli480",
                    NationalCode = "1475369514",
                    FirstName = "Asghar",
                    LastName = "Bogari",
                    BirthPlaceId = 8,
                    BirthDate = new DateTime(1975, 05, 12),
                    FatherName = "Akbar",
                    NationalityId = 4,
                    Age = 16
                });
        }
    }
}
