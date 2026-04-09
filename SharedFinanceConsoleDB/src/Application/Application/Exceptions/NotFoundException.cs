namespace SharedFinanceConsoleDB.Application.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
        // User
        public static string UserNotFound = "User not found";

        // Account
        public static string AccountNotFound = "Account not found";
    }
}
