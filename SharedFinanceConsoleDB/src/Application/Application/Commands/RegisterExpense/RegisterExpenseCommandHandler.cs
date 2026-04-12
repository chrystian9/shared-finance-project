using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Exceptions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;

namespace SharedFinanceConsoleDB.Application.Commands.RegisterExpense
{
    public class RegisterExpenseCommandHandler(IAccountRepository accountRepository) : IRequestHandler<RegisterExpenseCommand, Unit>
    {
        public Unit Handle(RegisterExpenseCommand request)
        {
            var payerAccount = accountRepository.GetByGuid(request.PayerAccountGuid)
                ?? throw new NotFoundException(NotFoundException.AccountNotFound);

            var counterparties = accountRepository
                .Where((a) => request.CounterpartiesPercentageByGuid.Keys.Contains(a.Guid))
                .Select((a) => new TransactionCounterparty(a, request.CounterpartiesPercentageByGuid[a.Guid]));

            payerAccount.RegisterExpense(request.TotalValue, request.Description, counterparties);

            accountRepository.SaveChanges();

            return Unit.Value;
        }
    }
}
