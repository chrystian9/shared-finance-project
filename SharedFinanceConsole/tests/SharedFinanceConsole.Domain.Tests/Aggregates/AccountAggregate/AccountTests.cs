using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsole.Domain.Common.DomainException;

namespace SharedFinanceConsole.Domain.Tests.Aggregates.AccountAggregate;

public class AccountTests
{
    [Fact]
    public void Constructor_SetsUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var account = new Account(userId);

        // Assert
        Assert.Equal(userId, account.UserId);
    }

    [Fact]
    public void GetBalance_ReturnsSumOfTransactions()
    {
        // Arrange
        var account = new Account(Guid.NewGuid());
        account.RegisterExpense(100m, "Expense", []);
        account.RegisterTransfer(50m, "Transfer", Guid.NewGuid());

        // Act
        var expectedBalance = account.Transactions.Sum(t => t.Value);
        var actualBalance = account.GetBalance();

        // Assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void RegisterExpense_AddsExpenseTransaction()
    {
        // Arrange
        var account = new Account(Guid.NewGuid());

        // Act
        account.RegisterExpense(100m, "Expense", []);

        // Assert
        Assert.Single(account.Transactions);
        Assert.Equal(-100m, account.Transactions.First().Value);
    }

    [Fact]
    public void RegisterExpense_AddsReceivableTransactionsForCounterparties()
    {
        // Arrange
        var account = new Account(Guid.NewGuid());
        var counterparties = new[]
        {
            new TransactionCounterparty(Guid.NewGuid(), 0.5m),
            new TransactionCounterparty(Guid.NewGuid(), 0.5m)
        };

        // Act
        account.RegisterExpense(100m, "Expense", counterparties);

        // Assert
        Assert.Equal(3, account.Transactions.Count);
        Assert.Contains(account.Transactions, t => t.Type == ETransactionType.RECEIVABLE);
    }

    [Fact]
    public void RegisterExpense_ThrowsWhenTotalValueIsZeroOrNegative()
    {
        // Arrange
        var account = new Account(Guid.NewGuid());

        // Act & Assert
        Assert.Throws<DomainException>(() =>
            account.RegisterExpense(0m, "Expense", Enumerable.Empty<TransactionCounterparty>()));
        Assert.Throws<DomainException>(() =>
            account.RegisterExpense(-10m, "Expense", Enumerable.Empty<TransactionCounterparty>()));
    }

    [Fact]
    public void RegisterTransfer_AddsTransferOutTransaction()
    {
        // Arrange
        var account = new Account(Guid.NewGuid());
        var counterpartyId = Guid.NewGuid();

        // Act
        account.RegisterTransfer(50m, "Transfer", counterpartyId);

        // Assert
        Assert.Single(account.Transactions);
        var transaction = account.Transactions.First();
        Assert.Equal(ETransactionType.TRANSFER_OUT, transaction.Type);
        Assert.Equal(-50m, transaction.Value);
        Assert.Equal(counterpartyId, transaction.Counterparty);
    }
}
