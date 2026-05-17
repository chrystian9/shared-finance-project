using SharedFinanceConsoleDB.Application.Commands.AddUser;
using SharedFinanceConsoleDB.Infrastructure.Repositories;
using SharedFinanceConsoleDB.Integration.Tests.Fixtures;

namespace SharedFinanceConsoleDB.Integration.Tests.Application.Commands
{
    public class AddUserCommandHandlerTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly AddUserCommandHandler _handler;

        public AddUserCommandHandlerTests()
        {
            _fixture = new DatabaseFixture();

            var repository = new UserRepository(_fixture.DbContext);
            _handler = new AddUserCommandHandler(_fixture.UnitOfWork, repository);
        }

        [Fact]
        public void Handle_ShouldAddUserToDatabase()
        {
            // Arrange
            var command = new AddUserCommand("Test");

            // Act
            var result = _handler.Handle(command);

            // Assert
            var user = _fixture.DbContext.Users.FirstOrDefault(u => u.Name == "Test" && u.Guid == result);
            Assert.NotNull(user);
        }

        [Fact]
        public void Handle_ShouldAddMultipleUsersToDatabase()
        {
            // Arrange
            var command1 = new AddUserCommand("Test1");
            var command2 = new AddUserCommand("Test2");

            // Act
            var result1 = _handler.Handle(command1);
            var result2 = _handler.Handle(command2);

            // Assert
            var user1 = _fixture.DbContext.Users.FirstOrDefault(u => u.Name == "Test1" && u.Guid == result1);
            var user2 = _fixture.DbContext.Users.FirstOrDefault(u => u.Name == "Test2" && u.Guid == result2);
            Assert.NotNull(user1);
            Assert.NotNull(user2);
        }

        [Fact]
        public void Handle_ShouldAddTwoUsersWithSameNameToDatabase()
        {
            // Arrange
            var command1 = new AddUserCommand("Test");
            var command2 = new AddUserCommand("Test");

            // Act
            var result1 = _handler.Handle(command1);
            var result2 = _handler.Handle(command2);

            // Assert
            var users = _fixture.DbContext.Users.Where(u => u.Name == "Test").ToList();
            Assert.Equal(2, users.Count);
            Assert.Contains(users, u => u.Guid == result1);
            Assert.Contains(users, u => u.Guid == result2);
        }
    }
}
