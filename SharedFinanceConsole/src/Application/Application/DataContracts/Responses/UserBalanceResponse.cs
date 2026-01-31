namespace SharedFinanceConsole.Application.DataContracts.Responses
{
    public record UserBalanceResponse
    {
        public string UserName { get; init; } = string.Empty;
        public Guid UserId { get; init; }
        public decimal Balance { get; init; }
    }
}
