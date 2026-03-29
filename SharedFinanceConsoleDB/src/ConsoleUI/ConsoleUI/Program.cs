using SharedFinanceConsoleDB.Application.Handlers.Commands;
using SharedFinanceConsoleDB.Application.Handlers.Queries;
using SharedFinanceConsoleDB.ConsoleUI;
using SharedFinanceConsoleDB.Database;
using SharedFinanceConsoleDB.Infrastructure.Repositories;

using var dbContext = new SharedFinanceDBContext();
dbContext.Database.EnsureCreated();

var userRepository = new UserRepository(dbContext);
var accountRepository = new AccountRepository(dbContext);

var appController = new AppController();

appController.RegisterHandler(new AddUserCommandHandler(userRepository));
appController.RegisterHandler(new AddAccountCommandHandler(accountRepository));
appController.RegisterHandler(new RegisterExpenseCommandHandler(accountRepository));
appController.RegisterHandler(new GetUsersBalancesQueryHandler(userRepository, accountRepository));

var ui = new Menu(appController);

ui.RunApp();

