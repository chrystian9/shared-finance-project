using SharedFinanceConsole.Application.Interfaces.Repositories;
using SharedFinanceConsole.Domain.Aggregates.UserAggregate;
using SharedFinanceConsole.Infrastructure.Repositories.Abstract;

namespace SharedFinanceConsole.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected override Guid GetEntityId(User entity) => entity.Id;
    }
}
