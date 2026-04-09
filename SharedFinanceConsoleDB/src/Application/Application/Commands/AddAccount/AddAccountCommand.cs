using SharedFinanceConsoleDB.Application.Abstractions;

namespace SharedFinanceConsoleDB.Application.Commands.AddAccount
{
    public record AddAccountCommand(Guid UserGuid) : ICommand<Guid>;
}
