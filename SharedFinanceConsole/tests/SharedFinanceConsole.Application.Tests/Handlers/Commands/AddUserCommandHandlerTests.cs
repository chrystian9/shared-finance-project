using NSubstitute;
using SharedFinanceConsole.Application.Commands;
using SharedFinanceConsole.Application.Handlers.Commands;
using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsole.Application.Tests.Handlers.Commands
{
    public class AddUserCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldAddUserAndReturnUserId()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(userRepository);
            var command = new AddUserCommand("Test User");

            // Act
            var result = handler.Handle(command);

            // Assert
            userRepository.Received(1).Add(Arg.Is<User>(u => u.Name == "Test User" && u.Id == result));
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
