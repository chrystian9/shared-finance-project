using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsole.Application.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
