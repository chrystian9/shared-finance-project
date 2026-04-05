using NSubstitute;
using SharedFinanceConsoleDB.Application.Commands.AddAccount;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Application.Tests.Handlers.Commands
{
    public class AddAccountCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldAddAccountAndReturnId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new AddAccountCommand(userId);
            var mockRepo = Substitute.For<IAccountRepository>();
            Account? addedAccount = null;
            mockRepo
                .When(r => r.Add(Arg.Any<Account>()))
                .Do(callInfo => addedAccount = callInfo.Arg<Account>());

            var handler = new AddAccountCommandHandler(mockRepo);

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.NotNull(addedAccount);
            Assert.Equal(userId, addedAccount.UserId);
            Assert.Equal(addedAccount.Id, result);
            mockRepo.Received(1).Add(Arg.Any<Account>());
        }
    }
}
