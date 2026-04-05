using SharedFinanceConsoleDB.Application.Abstractions;

namespace SharedFinanceConsoleDB.Application.Commands.AddUser
{
    public record AddUserCommand(string Name) : ICommand<Guid>;
}
