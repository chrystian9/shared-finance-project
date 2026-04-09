using SharedFinanceConsoleDB.Domain.Common.DomainException;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects
{
    public class TransactionCounterparty
    {
        public decimal Percentage { get; init; }
        public Account Account { get; init; }

        public TransactionCounterparty(Account account, decimal percentage)
        {
            if (percentage <= 0 || percentage > 1)
                throw new DomainException(DomainException.TransactionCounterpartyPercentageInvalid);

            Percentage = percentage;
            Account = account;
        }

        public decimal GetValue(decimal totalValue)
        {
            return decimal.Round(
                totalValue * Percentage,
                2,
                MidpointRounding.AwayFromZero
            );
        }
    }
}
