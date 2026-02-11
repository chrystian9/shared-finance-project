using ConsoleUI.UICommands.Interfaces;
using SharedFinanceConsole.Application.Queries;

namespace SharedFinanceConsole.ConsoleUI.MenuCommands
{
    public class ReportMenuCommand(AppController appController) : IMenuCommand
    {
        public string Label => "Report";

        public void Execute()
        {
            var usersBalances = appController.Send(new GetUsersBalancesQuery());

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
