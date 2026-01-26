namespace SharedFinanceConsole.Application.DataContracts.Responses
{
    public record UserBalanceResponse
    {
        public string UserName { get; init; }
        public Guid UserId { get; init; }
        public decimal Balance { get; init; }
    }
}
