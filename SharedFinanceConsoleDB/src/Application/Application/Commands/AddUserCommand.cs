using SharedFinanceConsoleDB.Application.Abstractions;

namespace SharedFinanceConsoleDB.Application.Commands
{
    public record AddUserCommand(string Name) : ICommand<Guid>;
}
