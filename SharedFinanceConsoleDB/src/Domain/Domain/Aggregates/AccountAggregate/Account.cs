using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Params;
using SharedFinanceConsoleDB.Domain.Common.DomainException;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate
{
    public class Account : Entity
    {
        public long UserId { get; init; }

        private readonly List<Transaction> _transactions = [];
        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        public Account(long userId)
        {
            UserId = userId;
        }

        public decimal GetBalance() => _transactions.Sum(t => t.Value);

        public void RegisterExpense(RegisterExpenseParams @params)
        {
            if (@params.TotalValue <= 0)
                throw new DomainException(DomainException.ExpenseTotalValueLessThanZero);

            _transactions.Add(Transaction.CreateExpense(Id, @params.TotalValue, @params.Description));

            foreach (var transactionCounterparty in @params.Counterparties)
            {
                _transactions.Add(
                    Transaction.CreateReceivable(
                        Id,
                        transactionCounterparty.GetValue(@params.TotalValue),
                        @params.Description,
                        transactionCounterparty.Account.Id)
                    );

                transactionCounterparty.Account.RegisterExpense(
                    new RegisterExpenseParams
                    {
                        TotalValue = transactionCounterparty.GetValue(@params.TotalValue),
                        Description = @params.Description,
                        Counterparties = []
                    }
                );
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
