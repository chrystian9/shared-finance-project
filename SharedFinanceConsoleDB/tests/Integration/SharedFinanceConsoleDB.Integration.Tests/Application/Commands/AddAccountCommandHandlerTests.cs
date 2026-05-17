using SharedFinanceConsoleDB.Application.Commands.AddAccount;
using SharedFinanceConsoleDB.Application.Exceptions;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
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
            var user = new User("Test User");
            _fixture.DbContext.Users.Add(user);
            _fixture.DbContext.SaveChanges();
            var command = new AddAccountCommand(user.Guid);

            // Act
            var result = _handler.Handle(command);

            // Assert
            var account = _fixture.DbContext.Accounts.FirstOrDefault(a => a.Guid == result && a.User.Guid == user.Guid);
            Assert.NotNull(account);
        }

        [Fact]
        public void Handle_ShouldAddMultipleAccountsToDatabase()
        {
            // Arrange
            var user = new User("Test User");
            _fixture.DbContext.Users.Add(user);
            _fixture.DbContext.SaveChanges();

            var command1 = new AddAccountCommand(user.Guid);
            var command2 = new AddAccountCommand(user.Guid);

            // Act
            var result1 = _handler.Handle(command1);
            var result2 = _handler.Handle(command2);

            // Assert
            var account1 = _fixture.DbContext.Accounts.FirstOrDefault(a => a.Guid == result1 && a.User.Guid == user.Guid);
            var account2 = _fixture.DbContext.Accounts.FirstOrDefault(a => a.Guid == result2 && a.User.Guid == user.Guid);
            Assert.NotNull(account1);
            Assert.NotNull(account2);
        }

        [Fact]
        public void Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var accountsBefore = _fixture.DbContext.Accounts.Count();
            var nonExistentUserGuid = Guid.NewGuid();
            var command = new AddAccountCommand(nonExistentUserGuid);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _handler.Handle(command));
            var accountsAfter = _fixture.DbContext.Accounts.Count();
            Assert.Equal(accountsBefore, accountsAfter);
            Assert.Empty(_fixture.DbContext.Accounts.Where(a => a.User.Guid == nonExistentUserGuid));
        }
    }
}
