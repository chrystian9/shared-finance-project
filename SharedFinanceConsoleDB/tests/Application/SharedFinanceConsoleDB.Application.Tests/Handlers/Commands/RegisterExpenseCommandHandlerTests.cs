using NSubstitute;
using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Commands.RegisterExpense;
using SharedFinanceConsoleDB.Application.Exceptions;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Tests.Handlers.Commands
{
    public class RegisterExpenseCommandHandlerTests
    {
        [Fact]
        public void Handle_Should_ReturnUnitValue_WhenRegisterExpenseAndTransfers()
        {
            // Arrange
            var payerUser = new User();
            var payerAccount = new Account(payerUser);

            var counterpartyUser1 = new User();
            var counterpartyAccount1 = new Account(counterpartyUser1);

            var counterpartyUser2 = new User();
            var counterpartyAccount2 = new Account(counterpartyUser2);

            const decimal totalValue = 100m;
            const decimal counterparty1Percentage = 0.5m;
            const decimal counterparty2Percentage = 0.5m;
            const string description = "Test expense";

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetByGuid(payerAccount.Guid).Returns(payerAccount);
            accountRepository.GetByGuid(counterpartyAccount1.Guid).Returns(counterpartyAccount1);
            accountRepository.GetByGuid(counterpartyAccount2.Guid).Returns(counterpartyAccount2);

            var counterpartiesPercentageByGuid = new Dictionary<Guid, decimal>
            {
                { counterpartyAccount1.Guid, counterparty1Percentage },
                { counterpartyAccount2.Guid, counterparty2Percentage }
            };

            var command = new RegisterExpenseCommand
            {
                PayerAccountGuid = payerAccount.Guid,
                TotalValue = totalValue,
                Description = description,
                CounterpartiesPercentageByGuid = counterpartiesPercentageByGuid
            };

            var handler = new RegisterExpenseCommandHandler(unitOfWork, accountRepository);

            // Act
            var result = handler.Handle(command);

            // Assert
            accountRepository.Received(1).GetByGuid(payerAccount.Guid);
            unitOfWork.Received(1).SaveChanges();
            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public void Handle_Should_ThrowNotFoundException_WhenPayerAccountDoesNotExist()
        {
            // Arrange
            var nonExistentPayerGuid = Guid.NewGuid();
            var counterpartiesPercentageByGuid = new Dictionary<Guid, decimal>
            {
                { Guid.NewGuid(), 1.0m }
            };

            var command = new RegisterExpenseCommand
            {
                PayerAccountGuid = nonExistentPayerGuid,
                TotalValue = 100m,
                Description = "Test expense",
                CounterpartiesPercentageByGuid = counterpartiesPercentageByGuid
            };

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetByGuid(nonExistentPayerGuid).Returns((Account?)null);

            var handler = new RegisterExpenseCommandHandler(unitOfWork, accountRepository);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => handler.Handle(command));
            unitOfWork.DidNotReceive().SaveChanges();
        }

        [Fact]
        public void Handle_Should_RegisterExpenseWithSingleCounterparty()
        {
            // Arrange
            var payerUser = new User();
            var payerAccount = new Account(payerUser);

            var counterpartyUser = new User();
            var counterpartyAccount = new Account(counterpartyUser);

            const decimal totalValue = 150m;
            const string description = "Lunch payment";

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetByGuid(payerAccount.Guid).Returns(payerAccount);
            accountRepository.Where(Arg.Any<System.Linq.Expressions.Expression<Func<Account, bool>>>())
                .Returns(x => [counterpartyAccount]);

            var counterpartiesPercentageByGuid = new Dictionary<Guid, decimal>
            {
                { counterpartyAccount.Guid, 1.0m }
            };

            var command = new RegisterExpenseCommand
            {
                PayerAccountGuid = payerAccount.Guid,
                TotalValue = totalValue,
                Description = description,
                CounterpartiesPercentageByGuid = counterpartiesPercentageByGuid
            };

            var handler = new RegisterExpenseCommandHandler(unitOfWork, accountRepository);

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.Equal(Unit.Value, result);
            accountRepository.Received(1).GetByGuid(payerAccount.Guid);
            accountRepository.Received(1).Where(Arg.Any<System.Linq.Expressions.Expression<Func<Account, bool>>>());
            unitOfWork.Received(1).SaveChanges();
        }

        [Fact]
        public void Handle_Should_CallRepositoryWhereWithCorrectPredicate()
        {
            // Arrange
            var payerUser = new User();
            var payerAccount = new Account(payerUser);

            var counterpartyAccount1 = new Account(new User());
            var counterpartyAccount2 = new Account(new User());

            var allAccounts = new List<Account> { payerAccount, counterpartyAccount1, counterpartyAccount2 };

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetByGuid(payerAccount.Guid).Returns(payerAccount);

            var counterpartiesPercentageByGuid = new Dictionary<Guid, decimal>
            {
                { counterpartyAccount1.Guid, 0.6m },
                { counterpartyAccount2.Guid, 0.4m }
            };

            accountRepository.Where(Arg.Any<System.Linq.Expressions.Expression<Func<Account, bool>>>())
                .Returns(x =>
                {
                    var expression = x.Arg<System.Linq.Expressions.Expression<Func<Account, bool>>>();
                    var predicate = expression.Compile();
                    return [.. allAccounts.Where(predicate)];
                });

            var command = new RegisterExpenseCommand
            {
                PayerAccountGuid = payerAccount.Guid,
                TotalValue = 200m,
                Description = "Shared dinner",
                CounterpartiesPercentageByGuid = counterpartiesPercentageByGuid
            };

            var handler = new RegisterExpenseCommandHandler(unitOfWork, accountRepository);

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.Equal(Unit.Value, result);
            accountRepository.Received(1).Where(Arg.Any<System.Linq.Expressions.Expression<Func<Account, bool>>>());
        }
    }
}
