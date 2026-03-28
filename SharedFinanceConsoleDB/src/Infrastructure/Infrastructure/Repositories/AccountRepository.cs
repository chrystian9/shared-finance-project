using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Infrastructure.Repositories.Abstract;

namespace SharedFinanceConsoleDB.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        protected override Guid GetEntityId(Account entity) => entity.Id;
    }
}
