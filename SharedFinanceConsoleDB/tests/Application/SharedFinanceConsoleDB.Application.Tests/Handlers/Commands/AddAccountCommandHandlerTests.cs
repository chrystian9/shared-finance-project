using NSubstitute;
using SharedFinanceConsoleDB.Application.Commands.AddAccount;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Tests.Handlers.Commands
{
    public class AddAccountCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldAddAccountAndReturnId()
        {
            // Arrange
            var user = new User("Test User");
            var command = new AddAccountCommand(user.Guid);
            var mockUserRepo = Substitute.For<IUserRepository>();
            var mockAccountRepo = Substitute.For<IAccountRepository>();

            mockUserRepo.GetByGuid(user.Guid).Returns(user);

            var handler = new AddAccountCommandHandler(mockUserRepo, mockAccountRepo);

            // Act
            var result = handler.Handle(command);

            // Assert
            mockAccountRepo.Received(1).Add(Arg.Is<Account>(a => a.UserId == 1));
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
