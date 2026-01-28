using SharedFinanceConsole.Application.Abstractions;

namespace SharedFinanceConsole.Application.Commands
{
    public record AddAccountCommand(Guid UserId) : ICommand<Guid>;
}
