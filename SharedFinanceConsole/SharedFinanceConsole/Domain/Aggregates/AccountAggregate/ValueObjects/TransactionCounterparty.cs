using SharedFinanceConsole.Domain.Common.DomainException;

namespace SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects
{
    public record TransactionCounterparty
    {
        public decimal Percentage { get; init; }
        public Guid UserId { get; init; }

        public TransactionCounterparty(Guid userId, decimal percentage)
        {
            if (percentage <= 0)
                throw new DomainException(DomainException.TransactionCounterpartyPercentageInvalid);

            Percentage = percentage;
            UserId = userId;
        }
    }
}
