using NSubstitute;
using SharedFinanceConsole.Application.Abstractions;
using SharedFinanceConsole.Application.Commands;
using SharedFinanceConsole.Application.Handlers.Commands;
using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;

namespace SharedFinanceConsole.Application.Tests.Handlers.Commands
{
    public class RegisterExpenseCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldRegisterExpenseAndTransfers()
        {
            // Arrange
            var payerAccountId = Guid.NewGuid();
            var counterpartyId1 = Guid.NewGuid();
            var counterpartyId2 = Guid.NewGuid();
            var totalValue = 100m;
            var description = "Test expense";

            var counterparty1 = new TransactionCounterparty(counterpartyId1, 0.5m);
            var counterparty2 = new TransactionCounterparty(counterpartyId2, 0.5m);
            
            var counterparties = new List<TransactionCounterparty> { counterparty1, counterparty2 };

            var payerAccount = Substitute.For<Account>(Guid.NewGuid());
            var counterpartyAccount1 = Substitute.For<Account>(Guid.NewGuid());
            var counterpartyAccount2 = Substitute.For<Account>(Guid.NewGuid());

            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetById(payerAccountId).Returns(payerAccount);
            accountRepository.GetById(counterpartyId1).Returns(counterpartyAccount1);
            accountRepository.GetById(counterpartyId2).Returns(counterpartyAccount2);

            var command = new RegisterExpenseCommand
            {
                PayerAccountId = payerAccountId,
                TotalValue = totalValue,
                Description = description,
                Counterparties = counterparties
            };

            var handler = new RegisterExpenseCommandHandler(accountRepository);

            // Act
            var result = handler.Handle(command);

            // Assert
            payerAccount.Received(1).RegisterExpense(totalValue, description, counterparties);
            accountRepository.Received(1).Save(payerAccount);

            counterpartyAccount1.Received(1).RegisterTransfer(50m, description, counterpartyId1);
            counterpartyAccount2.Received(1).RegisterTransfer(50m, description, counterpartyId2);
            accountRepository.Received(1).Save(counterpartyAccount1);
            accountRepository.Received(1).Save(counterpartyAccount2);

            Assert.Equal(Unit.Value, result);
        }
    }
}
