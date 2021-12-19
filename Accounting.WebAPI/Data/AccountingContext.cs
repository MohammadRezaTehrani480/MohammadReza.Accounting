using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;
using Accounting.WebAPI.EntityTypeConfiguration;
using Accounting.WebAPI.Enum;

namespace Accounting.WebAPI.Data
{
    public class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options)
        {
        }
        //public DbSet<RealPerson> RealPeople { get; set; }
        //public DbSet<LegalPerson> LegalPeople { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Lookup> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var tvContracts = context.Contracts.OfType<TvContract>().ToList();
            modelBuilder.Entity<RealPerson>();
            modelBuilder.Entity<LegalPerson>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingContext).Assembly);

            //modelBuilder.Seed();
        }
    }
}
