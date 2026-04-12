using NSubstitute;
using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.Commands.RegisterExpense;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;

namespace SharedFinanceConsoleDB.Application.Tests.Handlers.Commands
{
    public class RegisterExpenseCommandHandlerTests
    {
        [Fact]
        public void Handle_Should_ReturnUnitValue_WhenRegisterExpenseAndTransfers()
        {
            // Arrange
            var payerAccount = new Account(1);
            var counterpartyAccount1 = new Account(2);
            var counterpartyAccount2 = new Account(3);
            var totalValue = 100m;
            var description = "Test expense";

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetByGuid(payerAccount.Guid).Returns(payerAccount);
            accountRepository.GetByGuid(counterpartyAccount1.Guid).Returns(counterpartyAccount1);
            accountRepository.GetByGuid(counterpartyAccount2.Guid).Returns(counterpartyAccount2);

            var counterpartiesPercentageByGuid = new Dictionary<Guid, decimal>
            {
                { counterpartyAccount1.Guid, 0.5m },
                { counterpartyAccount2.Guid, 0.5m }
            };

            var command = new RegisterExpenseCommand
            {
                PayerAccountGuid = payerAccount.Guid,
                TotalValue = totalValue,
                Description = description,
                CounterpartiesPercentageByGuid = counterpartiesPercentageByGuid
            };

            var counterparties = new[] {
                new TransactionCounterparty(counterpartyAccount1, counterpartiesPercentageByGuid[counterpartyAccount1.Guid]),
                new TransactionCounterparty(counterpartyAccount1, counterpartiesPercentageByGuid[counterpartyAccount1.Guid])
            };

            var handler = new RegisterExpenseCommandHandler(unitOfWork, accountRepository);

            // Act
            var result = handler.Handle(command);

            // Assert
            accountRepository.Received(1).GetByGuid(payerAccount.Guid);
            accountRepository.Received(1).Where((a) => counterpartiesPercentageByGuid.Keys.Contains(a.Guid));

            payerAccount.Received(1).RegisterExpense(totalValue, description,
            [
                new(counterpartyAccount1, counterpartiesPercentageByGuid[counterpartyAccount1.Guid]),
                new(counterpartyAccount2, counterpartiesPercentageByGuid[counterpartyAccount2.Guid])
            ]);

            unitOfWork.Received(1).SaveChanges();

            Assert.Equal(Unit.Value, result);
        }
    }
}
