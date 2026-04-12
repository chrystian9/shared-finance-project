using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsoleDB.Domain.Common.DomainException;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Account : Entity
    {
        public long UserId { get; init; }

        private readonly List<Transaction> _transactions = [];
        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        public Account() { }

        public Account(long userId)
        {
            UserId = userId;
        }

        public decimal GetBalance() => _transactions.Sum(t => t.Value);

        public void RegisterExpense(decimal totalValue,
            string description,
            IEnumerable<TransactionCounterparty> counterparties)
        {
            if (totalValue <= 0)
                throw new DomainException(DomainException.ExpenseTotalValueLessThanZero);

            _transactions.Add(Transaction.CreateExpense(Id, totalValue, description));

            foreach (var transactionCounterparty in counterparties)
            {
                _transactions.Add(
                    Transaction.CreateReceivable(
                        Id,
                        transactionCounterparty.GetValue(totalValue),
                        description,
                        transactionCounterparty.Account.Id)
                    );

                transactionCounterparty.RegisterExpense(totalValue, description);
            }
        }

        public void RegisterTransfer(decimal value,
            string description,
            long userId)
        {
            _transactions.Add(
                Transaction.CreateTransferOut(
                    Id,
                    value,
                    description,
                    userId)
                );
        }

        public void RegisterDeposit(decimal value,
            string description)
        {
            _transactions.Add(
                Transaction.CreateDeposit(
                    Id,
                    value,
                    description)
                );
        }
    }
}
