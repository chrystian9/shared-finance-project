using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Application.Commands.AddAccount;
using SharedFinanceConsoleDB.Application.Commands.AddUser;
using SharedFinanceConsoleDB.Application.Commands.RegisterExpense;
using SharedFinanceConsoleDB.Application.Queries.GetUsersBalances;
using SharedFinanceConsoleDB.ConsoleUI;
using SharedFinanceConsoleDB.Database;
using SharedFinanceConsoleDB.Infrastructure.Repositories;

var connectionSqlite = new SqliteConnection("Data Source=shared_finance.db");
connectionSqlite.Open();

var options = new DbContextOptionsBuilder<SharedFinanceDBContext>()
    .UseSqlite(connectionSqlite)
    //.LogTo(Console.WriteLine)
    .Options;

using var dbContext = new SharedFinanceDBContext(options);
dbContext.Database.EnsureCreated();

var unitOfWork = new UnitOfWork(dbContext);
var userRepository = new UserRepository(dbContext);
var accountRepository = new AccountRepository(dbContext);

var appController = new AppController();

appController.RegisterHandler(new AddUserCommandHandler(unitOfWork, userRepository));
appController.RegisterHandler(new AddAccountCommandHandler(unitOfWork, userRepository, accountRepository));
appController.RegisterHandler(new RegisterExpenseCommandHandler(unitOfWork, accountRepository));
appController.RegisterHandler(new GetUsersBalancesQueryHandler(userRepository, accountRepository));

var ui = new Menu(appController);

ui.RunApp();

connectionSqlite.Close();
dbContext.Dispose();