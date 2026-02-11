using NSubstitute;
using SharedFinanceConsole.Application.Commands;
using SharedFinanceConsole.Application.Handlers.Commands;
using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsole.Application.Tests.Handlers.Commands
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
