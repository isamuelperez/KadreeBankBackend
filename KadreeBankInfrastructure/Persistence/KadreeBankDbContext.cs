using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankInfrastructure.Persistence
{
    public class KadreeBankDbContext : DbContext
    {
        public KadreeBankDbContext(
            DbContextOptions<KadreeBankDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<BankTransaction> Transactions => Set<BankTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KadreeBankDbContext).Assembly);
        }
    }
}
