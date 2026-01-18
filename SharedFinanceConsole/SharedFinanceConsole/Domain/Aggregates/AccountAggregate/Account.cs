using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsole.Domain.Common.DomainException;
using SharedFinanceConsole.Domain.Common.Entity;

namespace SharedFinanceConsole.Domain.Aggregates.AccountAggregate
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

            _transactions.Add(Transaction.CreateExpense(totalValue, description));

            foreach (var counterparty in counterparties)
            {
                var value = Decimal.Round(
                    totalValue * counterparty.Percentage,
                    2,
                    MidpointRounding.AwayFromZero
                );

                _transactions.Add
                    (Transaction.CreateReceivable(
                        value, 
                        description, 
                        counterparty.UserId)
                    );
            }
        }

        public void RegisterTransfer(decimal value,
            string description,
            Guid userId)
        {
            _transactions.Add(Transaction.CreateTransferOut(value, description, userId));
        }
    }
}
