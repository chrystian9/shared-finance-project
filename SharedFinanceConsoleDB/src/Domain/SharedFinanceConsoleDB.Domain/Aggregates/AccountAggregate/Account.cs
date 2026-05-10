using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
using SharedFinanceConsoleDB.Domain.Common.DomainException;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Account : Entity
    {
        public long UserId { get; init; }
        public virtual User User { get; init; }

        private readonly List<Transaction> _transactions = [];
        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        private readonly List<Transaction> _counterpartyTransactions = [];
        public IReadOnlyCollection<Transaction> CounterpartyTransactions => _counterpartyTransactions;

        public Account() { }

        public Account(User user)
        {
            User = user
                ?? throw new DomainException(DomainException.UserIsRequired);
        }

        public decimal GetBalance() => _transactions.Sum(t => t.Value);

        public void RegisterExpense(decimal totalValue,
            string description,
            IEnumerable<TransactionCounterparty> counterparties)
        {
            if (totalValue <= 0)
                throw new DomainException(DomainException.ExpenseTotalValueLessThanZero);

            _transactions.Add(Transaction.CreateExpense(totalValue, description, this));

            foreach (var transactionCounterparty in counterparties)
            {
                var transaction = Transaction.CreateReceivable(
                    transactionCounterparty.GetValue(totalValue),
                    description,
                    this,
                    transactionCounterparty.Account);

                _transactions.Add(transaction);

                transactionCounterparty.RegisterExpense(totalValue, description);
            }
        }

        public void RegisterTransfer(decimal value,
            string description,
            Account counterparty)
        {
            var transaction = Transaction.CreateTransferOut(
                value,
                description,
                this,
                counterparty);

            _transactions.Add(transaction);
        }

        public void RegisterDeposit(decimal value,
            string description)
        {
            var transaction = Transaction.CreateDeposit(
                value,
                description,
                this);

            _transactions.Add(transaction);
        }
    }
}
