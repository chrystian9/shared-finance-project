using SharedFinanceConsoleDB.Application.Abstractions;

namespace SharedFinanceConsoleDB.Application.Commands
{
    public record AddAccountCommand(Guid UserId) : ICommand<Guid>;
}
