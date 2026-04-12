using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Commands.AddUser
{
    public class AddUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository) : IRequestHandler<AddUserCommand, Guid>
    {
        public Guid Handle(AddUserCommand request)
        {
            var user = new User(request.Name);

            userRepository.Add(user);

            unitOfWork.SaveChanges();

            return user.Guid;
        }
    }
}
