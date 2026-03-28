using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
using SharedFinanceConsoleDB.Infrastructure.Repositories.Abstract;

namespace SharedFinanceConsoleDB.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected override Guid GetEntityId(User entity) => entity.Id;
    }
}
