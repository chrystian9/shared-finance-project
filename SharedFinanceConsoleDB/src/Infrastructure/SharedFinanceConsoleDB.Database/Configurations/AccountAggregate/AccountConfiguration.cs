using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Database.Configurations.AccountAggregate
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);

            var navigation = builder.Metadata.FindNavigation(nameof(Account.Transactions));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(a => a.CounterpartyTransactions)
                .WithOne(t => t.Counterparty)
                .HasForeignKey(t => t.CounterpartyId)
                .IsRequired(false);

            var counterpartyNavigation = builder.Metadata.FindNavigation(nameof(Account.CounterpartyTransactions));
            counterpartyNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
