using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;
using Accounting.WebAPI.EntityTypeConfiguration;
using Accounting.WebAPI.Enum;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Accounting.WebAPI.Data
{
    /*So this just another class that allows us to take advantages of identity services*/
    public class AccountingContext : IdentityDbContext<ApiUser>
    {
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Lookup> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RealPerson>();
            modelBuilder.Entity<LegalPerson>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingContext).Assembly);
        }
    }
}
