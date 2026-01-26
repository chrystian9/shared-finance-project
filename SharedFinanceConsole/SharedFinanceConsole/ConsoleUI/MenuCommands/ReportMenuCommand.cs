using SharedFinanceConsole.Application.Services;
using SharedFinanceConsole.ConsoleUI.Interfaces;

namespace SharedFinanceConsole.ConsoleUI.MenuCommands
{
    public class ReportMenuCommand(SharedFinanceAppService appService) : IMenuCommand
    {
        public string Label => "Report";

        public void Execute()
        {
            var usersBalances = appService.GetUsersBalances();

            if (!usersBalances.Any())
            {
                Console.WriteLine("No has users");
                return;
            }

            foreach (var userBalance in usersBalances)
            {
                Console.WriteLine($"{userBalance.UserName} (Id: {userBalance.UserId}) - Balance: {userBalance.Balance}");
            }
        }
    }
}
