using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsole.Domain.Common.DomainException;

namespace SharedFinanceConsole.Domain.Tests.Aggregates.AccountAggregate.ValueObjects
{
    public class TransactionCounterpartyTests
    {
        [Fact]
        public void Constructor_ValidPercentage_ShouldSetProperties()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            decimal percentage = 0.5m;

            // Act
            var counterparty = new TransactionCounterparty(accountId, percentage);

            // Assert
            Assert.Equal(accountId, counterparty.AccountId);
            Assert.Equal(percentage, counterparty.Percentage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        public void Constructor_InvalidPercentage_ShouldThrowDomainException(decimal invalidPercentage)
        {
            // Arrange
            var accountId = Guid.NewGuid();

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new TransactionCounterparty(accountId, invalidPercentage));
            Assert.Equal(DomainException.TransactionCounterpartyPercentageInvalid, ex.Message);
        }

        [Theory]
        [InlineData(100, 0.5, 50)]
        [InlineData(200, 0.25, 50)]
        [InlineData(123.45, 0.1, 12.35)]
        [InlineData(10, 1, 10)]
        public void GetValue_ShouldReturnRoundedValue(decimal totalValue, decimal percentage, decimal expected)
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var counterparty = new TransactionCounterparty(accountId, percentage);

            // Act
            var value = counterparty.GetValue(totalValue);

            // Assert
            Assert.Equal(expected, value);
        }
    }
}
