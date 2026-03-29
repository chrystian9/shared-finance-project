using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsoleDB.Domain.Common.DomainException;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Account : Entity
    {
        public Guid UserId { get; init; }

        private readonly List<Transaction> _transactions = [];
        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        public Account(Guid userId)
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

            foreach (var counterparty in counterparties)
            {
                _transactions.Add(
                    Transaction.CreateReceivable(
                        Id,
                        counterparty.GetValue(totalValue),
                        description,
                        counterparty.AccountId)
                    );
            }
        }

        public void RegisterTransfer(decimal value,
            string description,
            Guid userId)
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
