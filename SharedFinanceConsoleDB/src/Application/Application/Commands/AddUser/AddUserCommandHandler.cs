using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Commands.AddUser
{
    public class AddUserCommandHandler(IUserRepository userRepository) : IRequestHandler<AddUserCommand, Guid>
    {
        public Guid Handle(AddUserCommand request)
        {
            var user = new User(request.Name);

            userRepository.AddAndSaveChanges(user);

            return user.Guid;
        }
    }
}
