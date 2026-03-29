using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Database.Configurations.AccountAggregate
{
    public class AccountsConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId);

            var navigation = builder.Metadata.FindNavigation(nameof(Account.Transactions));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
