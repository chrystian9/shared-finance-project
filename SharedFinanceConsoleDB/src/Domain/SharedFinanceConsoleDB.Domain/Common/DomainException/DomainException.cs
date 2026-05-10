namespace SharedFinanceConsoleDB.Domain.Common.DomainException
{
    public class DomainException(string message) : Exception(message)
    {
        // User
        public static string UserNameEmpty = "User name is required in User";

        // Account
        public static string ExpenseTotalValueLessThanZero = "Expense must be positive";
        public static string UserIsRequired = "User is required in Account";

        // Transaction
        public static string AccountIsRequired = "Account is required in Transaction";

        // TransactionCounterparty
        public static string TransactionCounterpartyPercentageInvalid = "Percentage is invalid in TransactionCounterparty";
    }
}
