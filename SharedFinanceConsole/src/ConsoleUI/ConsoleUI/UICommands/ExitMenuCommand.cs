using ConsoleUI.UICommands.Interfaces;

namespace SharedFinanceConsole.ConsoleUI.MenuCommands
{
    public class ExitMenuCommand(AppController appController) : IMenuCommand
    {
        public string Label => "Exit";

        public void Execute() => appController.Stop();
    }
}
