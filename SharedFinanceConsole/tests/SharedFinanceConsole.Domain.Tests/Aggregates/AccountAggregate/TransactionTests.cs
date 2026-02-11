using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.Enum;

namespace SharedFinanceConsole.Domain.Tests.Aggregates.AccountAggregate
{
    public class TransactionTests
    {
        [Fact]
        public void CreateExpense_ShouldSetNegativeValueAndExpenseType()
        {
            // Arrange
            decimal value = 100m;
            string description = "Compra de material";

            // Act
            var transaction = Transaction.CreateExpense(value, description);

            // Assert
            Assert.Equal(-value, transaction.Value);
            Assert.Equal(ETransactionType.EXPENSE, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Null(transaction.Counterparty);
        }

        [Fact]
        public void CreateReceivable_ShouldSetPositiveValueAndReceivableType()
        {
            // Arrange
            decimal value = 200m;
            string description = "Recebimento de serviço";
            Guid counterparty = Guid.NewGuid();

            // Act
            var transaction = Transaction.CreateReceivable(value, description, counterparty);

            // Assert
            Assert.Equal(value, transaction.Value);
            Assert.Equal(ETransactionType.RECEIVABLE, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(counterparty, transaction.Counterparty);
        }

        [Fact]
        public void CreateTransferOut_ShouldSetNegativeValueAndTransferOutType()
        {
            // Arrange
            decimal value = 50m;
            string description = "Transferência para amigo";
            Guid counterparty = Guid.NewGuid();

            // Act
            var transaction = Transaction.CreateTransferOut(value, description, counterparty);

            // Assert
            Assert.Equal(-value, transaction.Value);
            Assert.Equal(ETransactionType.TRANSFER_OUT, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(counterparty, transaction.Counterparty);
        }

        [Fact]
        public void CreateTransferIn_ShouldSetPositiveValueAndTransferInType()
        {
            // Arrange
            decimal value = 75m;
            string description = "Transferência recebida";
            Guid counterparty = Guid.NewGuid();

            // Act
            var transaction = Transaction.CreateTransferIn(value, description, counterparty);

            // Assert
            Assert.Equal(value, transaction.Value);
            Assert.Equal(ETransactionType.TRANSFER_IN, transaction.Type);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(counterparty, transaction.Counterparty);
        }
    }
}
