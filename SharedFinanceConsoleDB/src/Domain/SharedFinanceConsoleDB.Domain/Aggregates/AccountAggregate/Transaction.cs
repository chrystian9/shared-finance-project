using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsoleDB.Domain.Common.DomainException;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Transaction : Entity
    {
        public decimal Value { get; init; }
        public ETransactionType Type { get; init; }
        public string Description { get; init; }

        public long AccountId { get; init; }
        public virtual Account Account { get; init; }

        public long? CounterpartyId { get; init; }
        public virtual Account? Counterparty { get; init; }

        public Transaction() { }

        public Transaction(decimal value,
            ETransactionType type,
            string description,
            Account account,
            Account? counterparty)
        {
            Account = account
                ?? throw new DomainException(DomainException.AccountIsRequired);

            Value = value;
            Type = type;
            Description = description;
            Counterparty = counterparty;
        }

        public static Transaction CreateDeposit(decimal value, string description, Account account)
        {
            return new(+value, ETransactionType.DEPOSIT, description, account, null);
        }

        public static Transaction CreateExpense(decimal value, string description, Account account)
        {
            return new(-value, ETransactionType.EXPENSE, description, account, null);
        }

        public static Transaction CreateReceivable(decimal value, string description, Account account, Account? counterpartAccount)
        {
            return new(+value, ETransactionType.RECEIVABLE, description, account, counterpartAccount);
        }

        public static Transaction CreateTransferOut(decimal value, string description, Account account, Account counterpartAccount)
        {
            return new(-value, ETransactionType.TRANSFER_OUT, description, account, counterpartAccount);
        }

        public static Transaction CreateTransferIn(decimal value, string description, Account account, Account counterpartAccount)
        {
            return new(+value, ETransactionType.TRANSFER_IN, description, account, counterpartAccount);
        }
    }
}
