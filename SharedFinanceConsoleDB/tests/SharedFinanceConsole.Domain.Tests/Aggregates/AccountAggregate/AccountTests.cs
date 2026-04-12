using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsoleDB.Domain.Common.DomainException;

namespace SharedFinanceConsoleDB.Domain.Tests.Aggregates.AccountAggregate;

public class AccountTests
{
    [Fact]
    public void Constructor_SetsUserId()
    {
        // Arrange
        var userId = 1;

        // Act
        var account = new Account(userId);

        // Assert
        Assert.Equal(userId, account.UserId);
        Assert.Empty(account.Transactions);
    }

    [Fact]
    public void GetBalance_ReturnsSumOfTransactions()
    {
        // Arrange
        var account = new Account(1);
        account.RegisterExpense(100m, "Expense", []);
        account.RegisterTransfer(50m, "Transfer", 2);

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
        var account = new Account(1);

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
        var account = new Account(1);
        var counterparties = new[]
        {
            new TransactionCounterparty(new Account(2), 0.5m),
            new TransactionCounterparty(new Account(3), 0.5m)
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
        var account = new Account(1);

        // Act & Assert
        Assert.Throws<DomainException>(() => account.RegisterExpense(0m, "Expense", []));
        Assert.Throws<DomainException>(() => account.RegisterExpense(-10m, "Expense", []));
    }

    [Fact]
    public void RegisterTransfer_AddsTransferOutTransaction()
    {
        // Arrange
        var account = new Account(1);
        var counterpartyId = 2;

        // Act
        account.RegisterTransfer(50m, "Transfer", counterpartyId);

        // Assert
        Assert.Single(account.Transactions);
        var transaction = account.Transactions.First();
        Assert.Equal(ETransactionType.TRANSFER_OUT, transaction.Type);
        Assert.Equal(-50m, transaction.Value);
        Assert.Equal(counterpartyId, transaction.CounterpartyId);
    }
}
