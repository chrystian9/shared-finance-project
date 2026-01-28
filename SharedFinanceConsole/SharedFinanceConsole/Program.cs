using SharedFinanceConsole.Application.Handlers.Commands;
using SharedFinanceConsole.Application.Handlers.Queries;
using SharedFinanceConsole.Application.Services;
using SharedFinanceConsole.ConsoleUI;
using SharedFinanceConsole.Infrastructure.Repositories;

var userRepository = new UserRepository();
var accountRepository = new AccountRepository();

var appService = new SharedFinanceAppService(userRepository, accountRepository);

var appController = new AppController();

appController.RegisterHandler(new AddUserCommandHandler(userRepository));
appController.RegisterHandler(new AddAccountCommandHandler(accountRepository));
appController.RegisterHandler(new RegisterExpenseCommandHandler(accountRepository));
appController.RegisterHandler(new GetUsersBalancesQueryHandler(userRepository, accountRepository));

var ui = new ConsoleUI(appController, appService);

ui.RunApp();