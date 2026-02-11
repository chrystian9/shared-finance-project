using NSubstitute;
using SharedFinanceConsole.Application.Handlers.Queries;
using SharedFinanceConsole.Application.Queries;
using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsole.Application.Tests.Handlers.Queries
{
    public class GetUsersBalancesQueryHandlerTests
    {
        [Fact]
        public void Handle_ReturnsUserBalances_ForExistingAccounts()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var accountRepository = Substitute.For<IAccountRepository>();

            var user1 = new User("Alice");
            var user2 = new User("Bob");

            userRepository.GetAll().Returns([user1, user2]);

            var account1 = new Account(user1.Id);
            account1.RegisterDeposit(100, "Deposit");

            var account2 = new Account(user2.Id);
            account2.RegisterDeposit(200, "Deposit");

            accountRepository.GetAll().Returns([account1, account2]);

            var handler = new GetUsersBalancesQueryHandler(userRepository, accountRepository);

            // Act
            var result = handler.Handle(new GetUsersBalancesQuery());

            // Assert
            var responses = result.ToList();
            Assert.Equal(2, responses.Count);

            Assert.Contains(responses, r => r.UserId == user1.Id && r.UserName == user1.Name && r.Balance == 100m);
            Assert.Contains(responses, r => r.UserId == user2.Id && r.UserName == user2.Name && r.Balance == 200m);
        }

        [Fact]
        public void Handle_ReturnsEmpty_WhenNoAccountsExist()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var accountRepository = Substitute.For<IAccountRepository>();

            var user1 = new User("Alice");

            userRepository.GetAll().Returns([user1]);
            accountRepository.GetAll().Returns([]);

            var handler = new GetUsersBalancesQueryHandler(userRepository, accountRepository);

            // Act
            var result = handler.Handle(new GetUsersBalancesQuery());

            // Assert
            Assert.Empty(result);
        }
    }
}
