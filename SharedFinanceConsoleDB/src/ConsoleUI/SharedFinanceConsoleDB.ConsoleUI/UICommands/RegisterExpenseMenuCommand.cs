using ConsoleUI.UICommands.Interfaces;
using SharedFinanceConsoleDB.Application.Commands.RegisterExpense;

namespace SharedFinanceConsoleDB.ConsoleUI.MenuCommands
{
    public class RegisterExpenseMenuCommand(AppController appController) : IMenuCommand
    {
        public string Label => "Register expense";

        public void Execute()
        {
            Console.WriteLine("Write user account ID:");

            var accountIdInput = Console.ReadLine();

            if (string.IsNullOrEmpty(accountIdInput)
                || !Guid.TryParse(accountIdInput, out var accountId))
            {
                Console.WriteLine("❌ Invalid ID!");
                return;
            }

            Console.WriteLine("Write total value:");

            var totalValueInput = Console.ReadLine();

            if (string.IsNullOrEmpty(totalValueInput)
                || !decimal.TryParse(totalValueInput, out var totalValue)
                || totalValue <= 0)
            {
                Console.WriteLine("❌ Invalid total value!");
                return;
            }

            Console.WriteLine("Write description:");

            var description = Console.ReadLine();

            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("❌ Invalid description!");
                return;
            }

            var loopControl = true;

            var counterpartiesPercentageByGuid = new Dictionary<Guid, decimal>();

            while (loopControl)
            {
                Console.WriteLine("Write counterparty account ID or press ENTER:");

                var counterpartyAccountGuidInput = Console.ReadLine();

                if (string.IsNullOrEmpty(counterpartyAccountGuidInput))
                    loopControl = false;
                else if (!Guid.TryParse(counterpartyAccountGuidInput, out var counterpartyAccountGuid))
                {
                    Console.WriteLine("❌ Invalid ID!");
                }
                else
                {
                    Console.WriteLine("Write counterparty percentage:");

                    var percentageInput = Console.ReadLine();

                    if (percentageInput == null
                        || !decimal.TryParse(percentageInput, out var percentage)
                        || percentage <= 0
                        || percentage > 1)
                        Console.WriteLine("❌ Invalid percentage!");
                    else
                        counterpartiesPercentageByGuid[counterpartyAccountGuid] = percentage;
                }
            }

            Console.WriteLine($@"Operation:
    AccountId: {accountId},
    Total value: {totalValue},
    Description: {description}");

            if (counterpartiesPercentageByGuid.Count > 0)
            {
                Console.WriteLine("Counterparties: [");
                foreach (var counterparty in counterpartiesPercentageByGuid)
                {
                    Console.WriteLine($"    {counterparty.Key}: {counterparty.Value * 100}%");
                }
                Console.WriteLine("]");
            }
            else
            {
                Console.WriteLine("Counterparties: []");
            }

            Console.WriteLine("Confirm operation? (S/N)");

            var confirm = Console.ReadLine();

            if (string.IsNullOrEmpty(confirm) || confirm != "S")
            {
                Console.WriteLine("Canceled operation");
                return;
            }

            appController.Send(new RegisterExpenseCommand()
            {
                PayerAccountGuid = accountId,
                TotalValue = totalValue,
                Description = description,
                CounterpartiesPercentageByGuid = counterpartiesPercentageByGuid
            });

            Console.WriteLine("Finished operation");
        }
    }
}
