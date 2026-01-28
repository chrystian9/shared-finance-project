using SharedFinanceConsole.Application.Abstractions;
using SharedFinanceConsole.Application.Commands;
using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsole.Application.Handlers
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
