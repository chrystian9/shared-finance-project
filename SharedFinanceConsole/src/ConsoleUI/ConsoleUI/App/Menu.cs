using ConsoleUI.UICommands.Interfaces;
using SharedFinanceConsole.ConsoleUI.MenuCommands;

namespace SharedFinanceConsole.ConsoleUI
{
    public class Menu(AppController appController)
    {
        private readonly IList<IMenuCommand> _commands = [
            new AddUserMenuCommand(appController),
            new ReportMenuCommand(appController),
            new RegisterExpenseMenuCommand(appController),
            new ExitMenuCommand(appController)
        ];

        public void RunApp()
        {
            ShowLogo();

            while (appController.IsRunning)
            {
                Console.Clear();

                ShowMenu();

                var input = ReadOption();

                if (int.TryParse(input, out int option) && option > 0 && option <= _commands.Count)
                    _commands[option - 1].Execute();
                else
                    Console.WriteLine("❌ Invalid option!");

                if (appController.IsRunning)
                    Pause();
            }

            ShowFinalReport();
        }

        #region Logo
        private static void ShowLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            TypeWrite(@"
               ███████╗██╗  ██╗ █████╗ ██████╗ ███████╗██████╗ 
               ██╔════╝██║  ██║██╔══██╗██╔══██╗██╔════╝██╔══██╗
               ███████╗███████║███████║██████╔╝█████╗  ██║  ██║
               ╚════██║██╔══██║██╔══██║██╔══██╗██╔══╝  ██║  ██║
               ███████║██║  ██║██║  ██║██║  ██║███████╗██████╔╝
               ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═════╝ 
                        SHARED FINANCE SYSTEM
                ");

            Console.ResetColor();
            ShowLoading();
        }

        private static void TypeWrite(string text)
        {
            foreach (var c in text)
            {
                Console.Write(c);
                Thread.Sleep(1);
            }
        }

        private static void ShowLoading()
        {
            Console.Write("Loading");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }

            Thread.Sleep(500);
        }
        #endregion

        #region Menu
        private void ShowMenu()
        {
            Console.WriteLine();

            for (int i = 0; i < _commands.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_commands[i].Label}");
            }

            Console.WriteLine();
            Console.Write("Select an option: ");
        }

        private static string ReadOption()
        {
            return Console.ReadLine()?.Trim() ?? "";
        }
        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        private void ShowFinalReport()
        {
            Console.Clear();
            Console.WriteLine("===== FINAL USERS BALANCE =====");

            _commands.OfType<ReportMenuCommand>().Single().Execute();

            Console.WriteLine();
            Console.WriteLine("System finished.");
        }
        #endregion
    }
}
