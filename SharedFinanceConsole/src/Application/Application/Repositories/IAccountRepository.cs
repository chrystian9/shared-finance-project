using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsole.Application.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
