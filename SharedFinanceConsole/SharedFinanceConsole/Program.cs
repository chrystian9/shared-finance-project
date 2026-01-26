using SharedFinanceConsole.Application.Services;
using SharedFinanceConsole.ConsoleUI;
using SharedFinanceConsole.Infrastructure.Repositories;

var userRepository = new UserRepository();
var accountRepository = new AccountRepository();

var appService = new SharedFinanceAppService(userRepository, accountRepository);

var appController = new AppController();

var ui = new ConsoleUI(appController, appService);

ui.RunApp();