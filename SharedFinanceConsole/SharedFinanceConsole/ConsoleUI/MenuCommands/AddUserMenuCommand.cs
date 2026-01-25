using SharedFinanceConsole.Application.Services;
using SharedFinanceConsole.ConsoleUI.Interfaces;

namespace SharedFinanceConsole.ConsoleUI.MenuCommands
{
    public class AddUserMenuCommand(SharedFinanceAppService appService) : IMenuCommand
    {
        public string Label => "Add user";

        public void Execute()
        {
            Console.WriteLine("Write user name:");

            var inputValue = Console.ReadLine();

            if (inputValue == null)
                return;

            var userId = appService.AddUser(inputValue);

            Console.WriteLine($"User ID: {userId}");

            var accountId = appService.AddAccount(userId);

            Console.WriteLine($"User account ID: {accountId}");
        }
    }
}
