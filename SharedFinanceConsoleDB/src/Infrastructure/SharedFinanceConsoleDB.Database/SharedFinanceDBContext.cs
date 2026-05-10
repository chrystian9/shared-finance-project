
using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Database
{
    public class SharedFinanceDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public SharedFinanceDBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=shared_finance.db")
                .LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
