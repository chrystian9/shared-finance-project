using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Application.Commands.AddAccount
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
