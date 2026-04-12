using NSubstitute;
using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Commands.AddUser;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Tests.Handlers.Commands
{
    public class AddUserCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldAddUserAndReturnUserId()
        {
            // Arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand("Test User");

            // Act
            var result = handler.Handle(command);

            // Assert
            userRepository.Received(1).Add(Arg.Is<User>(u => u.Name == "Test User" && u.Guid == result));
            unitOfWork.Received(1).SaveChanges();

            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
