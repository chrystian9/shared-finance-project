using SharedFinanceConsole.Application.Abstractions;

namespace SharedFinanceConsole.Application.Commands
{
    public record AddUserCommand(string Name) : ICommand<Guid>;
}
