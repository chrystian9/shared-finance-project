using SharedFinanceConsole.Application.Abstractions;
using SharedFinanceConsole.Application.Commands;
using SharedFinanceConsole.Application.Repositories;

namespace SharedFinanceConsole.Application.Handlers.Commands
{
    public class RegisterExpenseCommandHandler(IAccountRepository accountRepository) : IRequestHandler<RegisterExpenseCommand, Unit>
    {
        public Unit Handle(RegisterExpenseCommand request)
        {
            var payerAccount = accountRepository.GetById(request.PayerAccountId);

            payerAccount.RegisterExpense(request.TotalValue, request.Description, request.Counterparties);

            accountRepository.Save(payerAccount);

            foreach (var counterparty in request.Counterparties)
            {
                var counterpartyAccount = accountRepository.GetById(counterparty.AccountId);

                counterpartyAccount.RegisterTransfer(
                    counterparty.GetValue(request.TotalValue),
                    request.Description,
                    counterparty.AccountId
                );

                accountRepository.Save(counterpartyAccount);
            }

            return Unit.Value;
        }
    }
}
