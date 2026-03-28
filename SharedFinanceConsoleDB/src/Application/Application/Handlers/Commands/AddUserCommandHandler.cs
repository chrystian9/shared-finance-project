using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Commands;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Handlers.Commands
{
    public class AddUserCommandHandler(IUserRepository userRepository) : IRequestHandler<AddUserCommand, Guid>
    {
        public Guid Handle(AddUserCommand request)
        {
            var user = new User(request.Name);

            userRepository.Add(user);

            return user.Id;
        }
    }
}
