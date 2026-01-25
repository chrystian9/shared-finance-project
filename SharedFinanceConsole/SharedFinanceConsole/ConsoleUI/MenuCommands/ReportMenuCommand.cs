using SharedFinanceConsole.Application.Services;
using SharedFinanceConsole.ConsoleUI.Interfaces;

namespace SharedFinanceConsole.ConsoleUI.MenuCommands
{
    public class ReportMenuCommand(SharedFinanceAppService appService) : IMenuCommand
    {
        public string Label => "Report";

        public void Execute()
        {
            if (appService._usersById.Values.Count == 0)
            {
                Console.WriteLine("No has users");
                return;
            }

            foreach (var user in appService._usersById.Values)
            {
                var balance = appService._accountsByUserId[user.Id].GetBalance();
                Console.WriteLine($"{user.Name} (Id: {user.Id}) - Balance: {balance}");
            }
        }
    }
}
