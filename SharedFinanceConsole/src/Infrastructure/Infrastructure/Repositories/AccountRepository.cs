using SharedFinanceConsole.Application.Repositories;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Infrastructure.Repositories.Abstract;

namespace SharedFinanceConsole.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        protected override Guid GetEntityId(Account entity) => entity.Id;
    }
}
