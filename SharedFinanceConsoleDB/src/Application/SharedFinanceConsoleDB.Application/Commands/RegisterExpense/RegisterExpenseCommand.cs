using SharedFinanceConsoleDB.Application.Abstractions;

namespace SharedFinanceConsoleDB.Application.Commands.RegisterExpense
{
    public record RegisterExpenseCommand : ICommand<Unit>
    {
        public Guid PayerAccountGuid { get; init; }
        public decimal TotalValue { get; init; }
        public string Description { get; init; } = string.Empty;
        public IDictionary<Guid, decimal> CounterpartiesPercentageByGuid { get; init; }
    }
}
