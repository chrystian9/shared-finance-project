using SharedFinanceConsole.Application;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;

Console.WriteLine("Initial system");

var sharedFinanceApp = new SharedFinanceAppService();

Console.WriteLine("Add users...");

var user1Id = sharedFinanceApp.AddUser("User1");
sharedFinanceApp.AddAccount(user1Id);

var user2Id = sharedFinanceApp.AddUser("User2");
sharedFinanceApp.AddAccount(user2Id);

var user3Id = sharedFinanceApp.AddUser("User3");
sharedFinanceApp.AddAccount(user3Id);

foreach (var user in sharedFinanceApp._usersById.Values)
    Console.WriteLine($"User added: {user.Name} (Id: {user.Id}) (Balance: {sharedFinanceApp._accountsByUserId[user.Id].GetBalance()})");

Console.WriteLine("Add 100 expense to User1 with 40 percente counterparty by User2 and 20 percente counterparty by User3...");

sharedFinanceApp.RegisterExpense(user1Id, 
    100, 
    "Test expense", 
    [new TransactionCounterparty(user2Id, 0.4m), new TransactionCounterparty(user3Id, 0.2m)]);

foreach (var user in sharedFinanceApp._usersById.Values)
    Console.WriteLine($"User result: {user.Name} (Id: {user.Id}) (Balance: {sharedFinanceApp._accountsByUserId[user.Id].GetBalance()})");