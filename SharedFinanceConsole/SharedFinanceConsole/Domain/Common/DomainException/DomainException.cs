namespace SharedFinanceConsole.Domain.Common.DomainException
{
    public class DomainException(string message) : Exception(message)
    {
        // User
        public static string UserNameEmpty = "User name is required in User";

        // Account
        public static string ExpenseTotalValueLessThanZero = "Expense must be positive";   

        // TransactionCounterparty
        public static string TransactionCounterpartyPercentageInvalid = "Percentage is invalid in TransactionCounterparty";
    }
}
