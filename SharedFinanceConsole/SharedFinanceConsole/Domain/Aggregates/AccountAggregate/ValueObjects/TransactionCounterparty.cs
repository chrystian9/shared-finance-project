using SharedFinanceConsole.Domain.Common.DomainException;

namespace SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects
{
    public class TransactionCounterparty
    {
        public decimal Percentage { get; init; }
        public Guid AccountId { get; init; }

        public TransactionCounterparty(Guid userId, decimal percentage)
        {
            if (percentage <= 0 || percentage > 1)
                throw new DomainException(DomainException.TransactionCounterpartyPercentageInvalid);

            Percentage = percentage;
            AccountId = userId;
        }

        public decimal GetValue(decimal totalValue)
        {
            return Decimal.Round(
                totalValue * Percentage,
                2,
                MidpointRounding.AwayFromZero
            );
        }
    }
}
