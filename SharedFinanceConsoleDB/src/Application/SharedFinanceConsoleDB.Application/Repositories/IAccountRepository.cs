using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;

namespace SharedFinanceConsoleDB.Application.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
