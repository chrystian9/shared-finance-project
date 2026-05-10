using NSubstitute;
using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Commands.AddUser;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
using SharedFinanceConsoleDB.Domain.Common.DomainException;

namespace SharedFinanceConsoleDB.Application.Tests.Handlers.Commands
{
    public class AddUserCommandHandlerTests
    {
        [Fact]
        public void Handle_Should_AddUserAndReturnUserId()
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

        [Fact]
        public void Handle_Should_ThrowDomainException_WhenNameIsEmpty()
        {
            // Arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand(string.Empty);

            // Act & Assert
            Assert.Throws<DomainException>(() => handler.Handle(command));
            userRepository.DidNotReceive().Add(Arg.Any<User>());
            unitOfWork.DidNotReceive().SaveChanges();
        }

        [Fact]
        public void Handle_Should_ThrowDomainException_WhenNameIsNull()
        {
            // Arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand(null!);

            // Act & Assert
            Assert.Throws<DomainException>(() => handler.Handle(command));
            userRepository.DidNotReceive().Add(Arg.Any<User>());
            unitOfWork.DidNotReceive().SaveChanges();
        }

        [Fact]
        public void Handle_Should_ThrowDomainException_WhenNameIsOnlyWhitespace()
        {
            // Arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand("   ");

            // Act & Assert
            Assert.Throws<DomainException>(() => handler.Handle(command));
            userRepository.DidNotReceive().Add(Arg.Any<User>());
            unitOfWork.DidNotReceive().SaveChanges();
        }

        [Fact]
        public void Handle_Should_ReturnUniqueGuidForEachUser()
        {
            // Arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(unitOfWork, userRepository);

            var command1 = new AddUserCommand("User One");
            var command2 = new AddUserCommand("User Two");

            // Act
            var result1 = handler.Handle(command1);
            var result2 = handler.Handle(command2);

            // Assert
            Assert.NotEqual(result1, result2);
            Assert.NotEqual(Guid.Empty, result1);
            Assert.NotEqual(Guid.Empty, result2);
        }

        [Fact]
        public void Handle_ShouldCallAddBeforeSaveChanges()
        {
            // Arrange
            var callOrder = new List<string>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();

            userRepository.When(x => x.Add(Arg.Any<User>())).Do(_ => callOrder.Add("Add"));
            unitOfWork.When(x => x.SaveChanges()).Do(_ => callOrder.Add("SaveChanges"));

            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand("Test User");

            // Act
            handler.Handle(command);

            // Assert
            Assert.Equal(new[] { "Add", "SaveChanges" }, callOrder);
        }

        [Fact]
        public void Handle_ShouldPersistUserWithCorrectName()
        {
            // Arrange
            const string userName = "John Doe";
            var capturedUser = (User?)null;

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();

            userRepository.When(x => x.Add(Arg.Any<User>()))
                .Do(x => capturedUser = x.Arg<User>());

            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand(userName);

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.NotNull(capturedUser);
            Assert.Equal(userName, capturedUser!.Name);
            Assert.Equal(result, capturedUser!.Guid);
        }

        [Fact]
        public void Handle_ShouldHandleNameWithSpecialCharacters()
        {
            // Arrange
            const string userName = "José da Silva-Araújo";
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var userRepository = Substitute.For<IUserRepository>();
            var handler = new AddUserCommandHandler(unitOfWork, userRepository);
            var command = new AddUserCommand(userName);

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            userRepository.Received(1).Add(Arg.Is<User>(u => u.Name == userName));
            unitOfWork.Received(1).SaveChanges();
        }
    }
}
