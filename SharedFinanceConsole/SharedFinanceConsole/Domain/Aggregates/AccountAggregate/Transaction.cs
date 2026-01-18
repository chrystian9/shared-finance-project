using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.Enum;

namespace SharedFinanceConsole.Domain.Aggregates.AccountAggregate
{
    public class Transaction
    {
        public decimal Value { get; init; }
        public ETransactionType Type { get; init; }
        public string Description { get; init; }
        public Guid? Counterparty { get; init; }

        public Transaction(decimal value,
            ETransactionType type,
            string description,
            Guid? counterparty)
        {
            Value = value;
            Type = type;
            Description = description;
            Counterparty = counterparty;
        }

        public static Transaction CreateExpense(decimal value, string description)
        {
            return new(+value, ETransactionType.EXPENSE, description, null);
        }

        public static Transaction CreateReceivable(decimal value, string description, Guid? counterparty)
        {
            return new(-value, ETransactionType.RECEIVABLE, description, counterparty);
        }

        public static Transaction CreateTransferIn(decimal value, string description, Guid counterparty)
        {
            return new(+value, ETransactionType.TRANSFER_IN, description, counterparty);
        }

        public static Transaction CreateTransferOut(decimal value, string description, Guid counterparty)
        {
            return new(+value, ETransactionType.TRANSFER_IN, description, counterparty);
        }
    }
}
