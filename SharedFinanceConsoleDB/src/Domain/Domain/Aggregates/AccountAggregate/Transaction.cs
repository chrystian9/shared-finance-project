using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Transaction : Entity
    {
        public decimal Value { get; init; }
        public ETransactionType Type { get; init; }
        public string Description { get; init; }
        public Guid? Counterparty { get; init; }

        public Guid AccountId { get; init; }

        public Transaction(Guid accountId,
            decimal value,
            ETransactionType type,
            string description,
            Guid? counterparty)
        {
            Value = value;
            Type = type;
            Description = description;
            Counterparty = counterparty;
            AccountId = accountId;
        }

        public static Transaction CreateDeposit(Guid accountId, decimal value, string description)
        {
            return new(accountId, +value, ETransactionType.DEPOSIT, description, null);
        }

        public static Transaction CreateExpense(Guid accountId, decimal value, string description)
        {
            return new(accountId, -value, ETransactionType.EXPENSE, description, null);
        }

        public static Transaction CreateReceivable(Guid accountId, decimal value, string description, Guid? counterpartyUserId)
        {
            return new(accountId, +value, ETransactionType.RECEIVABLE, description, counterpartyUserId);
        }

        public static Transaction CreateTransferOut(Guid accountId, decimal value, string description, Guid counterpartyUserId)
        {
            return new(accountId, -value, ETransactionType.TRANSFER_OUT, description, counterpartyUserId);
        }

        public static Transaction CreateTransferIn(Guid accountId, decimal value, string description, Guid counterpartyUserId)
        {
            return new(accountId, +value, ETransactionType.TRANSFER_IN, description, counterpartyUserId);
        }
    }
}
