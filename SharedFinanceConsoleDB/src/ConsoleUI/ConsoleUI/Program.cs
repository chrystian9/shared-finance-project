using SharedFinanceConsoleDB.Application.Handlers.Commands;
using SharedFinanceConsoleDB.Application.Handlers.Queries;
using SharedFinanceConsoleDB.ConsoleUI;
using SharedFinanceConsoleDB.Infrastructure.Repositories;

var userRepository = new UserRepository();
var accountRepository = new AccountRepository();

var appController = new AppController();

appController.RegisterHandler(new AddUserCommandHandler(userRepository));
appController.RegisterHandler(new AddAccountCommandHandler(accountRepository));
appController.RegisterHandler(new RegisterExpenseCommandHandler(accountRepository));
appController.RegisterHandler(new GetUsersBalancesQueryHandler(userRepository, accountRepository));

var ui = new Menu(appController);

ui.RunApp();