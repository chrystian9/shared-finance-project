using SharedFinanceConsole.Application;

namespace SharedFinanceConsole.AppUI
{
    public class AppUI
    {
        public void RunApp()
        {
            Console.WriteLine("Initial system");

            var sharedFinanceApp = new SharedFinanceAppService();

            var systemControl = true;

            while (systemControl)
            {
                Console.WriteLine();
                Console.WriteLine("Menu");
                Console.WriteLine("1. Add user");
                Console.WriteLine("2. Remove user");
                Console.WriteLine("3. User balance");
                Console.WriteLine("4. Add expense to user");
                Console.WriteLine("5. Add receivable to user");
                Console.WriteLine("6. Create transfer between users");
                Console.WriteLine("7. Exit");

                var value = Console.ReadLine();

                switch (value)
                {
                    case "1": AddUser(sharedFinanceApp); break;
                    case "2": RemoveUser(sharedFinanceApp); break;
                    case "3": UserBalance(sharedFinanceApp); break;
                    case "4": AddExpenseToUser(sharedFinanceApp); break;
                    case "5": AddReceivableToUser(sharedFinanceApp); break;
                    case "6": CreateTransferBetweenUsers(sharedFinanceApp); break;
                    case "7": systemControl = false; break;
                }
            }

            foreach (var user in sharedFinanceApp._usersById.Values)
                Console.WriteLine($"User result: {user.Name} (Id: {user.Id}) (Balance: {sharedFinanceApp._accountsByUserId[user.Id].GetBalance()})");

            Console.WriteLine("Finished system...");
        }

        private void AddUser(SharedFinanceAppService sharedFinanceApp)
        {
            Console.WriteLine("Write user name:");

            var inputValue = Console.ReadLine();

            if (inputValue == null)
                return;

            var userId = sharedFinanceApp.AddUser(inputValue);

            Console.WriteLine($"User ID: {userId}");

            var accountId = sharedFinanceApp.AddAccount(userId);

            Console.WriteLine($"User account ID: {accountId}");
        }

        private void RemoveUser(SharedFinanceAppService sharedFinanceApp)
        {
            try
            {
                Console.WriteLine("Write user ID:");

                var inputValue = Console.ReadLine();

                if (inputValue == null)
                    return;

                // sharedFinanceApp.RemoveUser(inputValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in process: {ex.Message}");
            }
        }

        private void UserBalance(SharedFinanceAppService sharedFinanceApp)
        {
            // TODO
        }

        private void AddExpenseToUser(SharedFinanceAppService sharedFinanceApp)
        {
            // TODO
        }

        private void AddReceivableToUser(SharedFinanceAppService sharedFinanceApp)
        {
            // TODO
        }

        private void CreateTransferBetweenUsers(SharedFinanceAppService sharedFinanceApp)
        {
            // TODO
        }
    }
}
