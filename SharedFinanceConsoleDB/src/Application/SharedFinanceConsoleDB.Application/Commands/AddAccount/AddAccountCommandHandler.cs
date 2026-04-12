using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Exceptions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Application.Commands.AddAccount
{
    public class AddAccountCommandHandler(IUserRepository userRepository, IAccountRepository accountRepository) : IRequestHandler<AddAccountCommand, Guid>
    {
        public Guid Handle(AddAccountCommand request)
        {
            var user = userRepository.GetByGuid(request.UserGuid)
                ?? throw new NotFoundException(NotFoundException.UserNotFound);

            var account = new Account(user.Id);

            accountRepository.AddAndSaveChanges(account);

            return account.Guid;
        }
    }
}
