using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Domain.Tests.Aggregates.AccountAggregate
{
    public class TransactionTests
    {
        [Fact]
        public void CreateExpense_ShouldSetNegativeValueAndExpenseType()
        {
            // Arrange
            decimal value = 100m;
            string description = "Compra de material";

            var user = new User();
            var account = new Account(user);

            // Act
            var transaction = Transaction.CreateExpense(value, description, account);

            // Assert
            Assert.Equal(-value, transaction.Value);
            Assert.Equal(ETransactionType.EXPENSE, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(account, transaction.Account);
            Assert.Null(transaction.CounterpartyId);
        }

        [Fact]
        public void CreateReceivable_ShouldSetPositiveValueAndReceivableType()
        {
            // Arrange
            decimal value = 200m;
            string description = "Recebimento de serviço";

            var user = new User();
            var account = new Account(user);

            var counterpartyUser = new User();
            var counterpartyAccount = new Account(counterpartyUser);

            // Act
            var transaction = Transaction.CreateReceivable(value, description, account, counterpartyAccount);

            // Assert
            Assert.Equal(value, transaction.Value);
            Assert.Equal(ETransactionType.RECEIVABLE, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(account, transaction.Account);
            Assert.Equal(counterpartyAccount, transaction.Counterparty);
        }

        [Fact]
        public void CreateTransferOut_ShouldSetNegativeValueAndTransferOutType()
        {
            // Arrange
            decimal value = 50m;
            string description = "Transferência para amigo";

            var user = new User();
            var account = new Account(user);

            var counterpartyUser = new User();
            var counterpartyAccount = new Account(counterpartyUser);

            // Act
            var transaction = Transaction.CreateTransferOut(value, description, account, counterpartyAccount);

            // Assert
            Assert.Equal(-value, transaction.Value);
            Assert.Equal(ETransactionType.TRANSFER_OUT, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(account, transaction.Account);
            Assert.Equal(counterpartyAccount, transaction.Counterparty);
        }

        [Fact]
        public void CreateTransferIn_ShouldSetPositiveValueAndTransferInType()
        {
            // Arrange
            decimal value = 75m;
            string description = "Transferência recebida";

            var user = new User();
            var account = new Account(user);

            var counterpartyUser = new User();
            var counterpartyAccount = new Account(counterpartyUser);

            // Act
            var transaction = Transaction.CreateTransferIn(value, description, account, counterpartyAccount);

            // Assert
            Assert.Equal(value, transaction.Value);
            Assert.Equal(ETransactionType.TRANSFER_IN, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(account, transaction.Account);
            Assert.Equal(counterpartyAccount, transaction.Counterparty);
        }
    }
}
