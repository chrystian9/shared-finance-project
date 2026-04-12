using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Database.Configurations.AccountAggregate
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne<Account>()
                .WithMany()
                .HasForeignKey(t => t.AccountId);
        }
    }
}
