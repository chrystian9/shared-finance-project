using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Params
{
    public record RegisterExpenseParams
    {
        public decimal TotalValue { get; init; }
        public string Description { get; init; }
        public IEnumerable<TransactionCounterparty> Counterparties { get; init; }
    }
}