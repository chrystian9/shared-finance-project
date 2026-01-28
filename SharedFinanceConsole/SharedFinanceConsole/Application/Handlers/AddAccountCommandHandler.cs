using SharedFinanceConsole.Application.Abstractions;
using SharedFinanceConsole.Application.Commands;
using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsole.Application.Handlers
{
    public class AddAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<AddAccountCommand, Guid>
    {
        public Guid Handle(AddAccountCommand request)
        {
            var account = new Account(request.UserId);

            accountRepository.Add(account);

            return account.Id;
        }
    }
}
