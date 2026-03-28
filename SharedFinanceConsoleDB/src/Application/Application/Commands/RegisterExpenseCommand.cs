using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;

namespace SharedFinanceConsoleDB.Application.Commands
{
    public record RegisterExpenseCommand : ICommand<Unit>
    {
        public Guid PayerAccountId { get; init; }
        public decimal TotalValue { get; init; }
        public string Description { get; init; } = string.Empty;
        public IEnumerable<TransactionCounterparty> Counterparties { get; init; } = [];
    }
}
