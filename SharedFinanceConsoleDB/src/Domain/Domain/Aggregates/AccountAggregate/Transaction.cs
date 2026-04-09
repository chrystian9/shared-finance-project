using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Transaction : Entity
    {
        public decimal Value { get; init; }
        public ETransactionType Type { get; init; }
        public string Description { get; init; }

        public long? CounterpartyId { get; init; }
        public long AccountId { get; init; }

        public Transaction(long accountId,
            decimal value,
            ETransactionType type,
            string description,
            long? counterpartyId)
        {
            Value = value;
            Type = type;
            Description = description;
            CounterpartyId = counterpartyId;
            AccountId = accountId;
        }

        public static Transaction CreateDeposit(long accountId, decimal value, string description)
        {
            return new(accountId, +value, ETransactionType.DEPOSIT, description, null);
        }

        public static Transaction CreateExpense(long accountId, decimal value, string description)
        {
            return new(accountId, -value, ETransactionType.EXPENSE, description, null);
        }

        public static Transaction CreateReceivable(long accountId, decimal value, string description, long? counterpartAccountId)
        {
            return new(accountId, +value, ETransactionType.RECEIVABLE, description, counterpartAccountId);
        }

        public static Transaction CreateTransferOut(long accountId, decimal value, string description, long counterpartAccountId)
        {
            return new(accountId, -value, ETransactionType.TRANSFER_OUT, description, counterpartAccountId);
        }

        public static Transaction CreateTransferIn(long accountId, decimal value, string description, long counterpartAccountId)
        {
            return new(accountId, +value, ETransactionType.TRANSFER_IN, description, counterpartAccountId);
        }
    }
}
