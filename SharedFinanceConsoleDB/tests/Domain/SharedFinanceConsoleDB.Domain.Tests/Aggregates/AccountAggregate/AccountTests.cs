using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.Enum;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
using SharedFinanceConsoleDB.Domain.Common.DomainException;

namespace SharedFinanceConsoleDB.Domain.Tests.Aggregates.AccountAggregate;

public class AccountTests
{
    [Fact]
    public void Constructor_SetsUserId()
    {
        // Arrange
        var user = new User();

        // Act
        var account = new Account(user);

        // Assert
        Assert.Equal(user.Guid, account.User.Guid);
        Assert.Empty(account.Transactions);
    }

    [Fact]
    public void GetBalance_ReturnsSumOfTransactions()
    {
        // Arrange
        var user = new User();
        var userCounterparty = new User();

        var account = new Account(user);
        var accountCounterparty = new Account(userCounterparty);

        account.RegisterExpense(100m, "Expense", []);
        account.RegisterTransfer(50m, "Transfer", accountCounterparty);

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
        var user = new User();
        var account = new Account(user);

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
        var user = new User();
        var account = new Account(user);

        var userCounterparty1 = new User();
        var userCounterparty2 = new User();

        var accountCounterparty1 = new Account(userCounterparty1);
        var accountCounterparty2 = new Account(userCounterparty2);

        var counterparties = new[]
        {
            new TransactionCounterparty(accountCounterparty1, 0.5m),
            new TransactionCounterparty(accountCounterparty2, 0.5m)
        };

        // Act
        account.RegisterExpense(100m, "Expense", counterparties);

        // Assert
        Assert.Equal(3, account.Transactions.Count);
        Assert.Contains(account.Transactions, t => t.Type == ETransactionType.RECEIVABLE);

        Assert.Single(accountCounterparty1.Transactions);
        var transactionCounterparty1 = accountCounterparty1.Transactions.First();
        Assert.Equal(ETransactionType.EXPENSE, transactionCounterparty1.Type);

        Assert.Single(accountCounterparty2.Transactions);
        var transactionCounterparty2 = accountCounterparty2.Transactions.First();
        Assert.Equal(ETransactionType.EXPENSE, transactionCounterparty2.Type);
    }

    [Fact]
    public void RegisterExpense_ThrowsWhenTotalValueIsZeroOrNegative()
    {
        // Arrange
        var user = new User();
        var account = new Account(user);

        // Act & Assert
        Assert.Throws<DomainException>(() => account.RegisterExpense(0m, "Expense", []));
        Assert.Throws<DomainException>(() => account.RegisterExpense(-10m, "Expense", []));
    }

    [Fact]
    public void RegisterTransfer_AddsTransferOutTransaction()
    {
        // Arrange
        var user = new User();
        var account = new Account(user);

        var userCounterparty = new User();
        var accountCounterparty = new Account(userCounterparty);

        // Act
        account.RegisterTransfer(50m, "Transfer", accountCounterparty);

        // Assert
        Assert.Single(account.Transactions);
        var transaction = account.Transactions.First();
        Assert.Equal(ETransactionType.TRANSFER_OUT, transaction.Type);
        Assert.Equal(-50m, transaction.Value);
        Assert.Equal(accountCounterparty, transaction.Counterparty);
    }
}
