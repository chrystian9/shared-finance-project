using SharedFinanceConsoleDB.Application.Commands.AddAccount;
using SharedFinanceConsoleDB.Application.Exceptions;
using SharedFinanceConsoleDB.Infrastructure.Repositories;
using SharedFinanceConsoleDB.Integration.Tests.Fixtures;

namespace SharedFinanceConsoleDB.Integration.Tests.Application.Commands
{
    public class AddAccountCommandHandlerTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly AddAccountCommandHandler _handler;

        public AddAccountCommandHandlerTests()
        {
            _fixture = new DatabaseFixture();

            var userRepository = new UserRepository(_fixture.DbContext);
            var accountRepository = new AccountRepository(_fixture.DbContext);
            _handler = new AddAccountCommandHandler(_fixture.UnitOfWork, userRepository, accountRepository);
        }

        [Fact]
        public void Handle_ShouldAddAccountToDatabase()
        {
            // Arrange
            var userGuid = _fixture.DbContext.Users.First().Guid;
            var command = new AddAccountCommand(userGuid);

            // Act
            var result = _handler.Handle(command);

            // Assert
            var account = _fixture.DbContext.Accounts.FirstOrDefault(a => a.Guid == result && a.User.Guid == userGuid);
            Assert.NotNull(account);
        }

        [Fact]
        public void Handle_ShouldAddMultipleAccountsToDatabase()
        {
            // Arrange
            var userGuid = _fixture.DbContext.Users.First().Guid;
            var command1 = new AddAccountCommand(userGuid);
            var command2 = new AddAccountCommand(userGuid);

            // Act
            var result1 = _handler.Handle(command1);
            var result2 = _handler.Handle(command2);

            // Assert
            var account1 = _fixture.DbContext.Accounts.FirstOrDefault(a => a.Guid == result1 && a.User.Guid == userGuid);
            var account2 = _fixture.DbContext.Accounts.FirstOrDefault(a => a.Guid == result2 && a.User.Guid == userGuid);
            Assert.NotNull(account1);
            Assert.NotNull(account2);
        }

        [Fact]
        public void Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentUserGuid = Guid.NewGuid();
            var command = new AddAccountCommand(nonExistentUserGuid);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _handler.Handle(command));
        }
    }
}
