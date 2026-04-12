using ConsoleUI.UICommands.Interfaces;
using SharedFinanceConsoleDB.Application.Commands.AddAccount;
using SharedFinanceConsoleDB.Application.Commands.AddUser;

namespace SharedFinanceConsoleDB.ConsoleUI.MenuCommands
{
    public class AddUserMenuCommand(AppController appController) : IMenuCommand
    {
        public string Label => "Add user";

        public void Execute()
        {
            Console.WriteLine("Write user name:");

            var inputValue = Console.ReadLine();

            if (inputValue == null)
                return;

            var userId = appController.Send(new AddUserCommand(inputValue));

            Console.WriteLine($"User ID: {userId}");

            var accountId = appController.Send(new AddAccountCommand(userId));

            Console.WriteLine($"User account ID: {accountId}");
        }
    }
}
