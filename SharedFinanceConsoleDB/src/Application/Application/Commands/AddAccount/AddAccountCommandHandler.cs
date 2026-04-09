using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Exceptions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Application.Commands.AddAccount
{
    public class AddAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<AddAccountCommand, Guid>
    {
        public Guid Handle(AddAccountCommand request)
        {
            var userAccount = accountRepository.GetByGuid(request.UserGuid)
                ?? throw new NotFoundException(NotFoundException.UserNotFound);

            var account = new Account(userAccount.Id);

            accountRepository.AddAndSaveChanges(account);

            return account.Guid;
        }
    }
}
