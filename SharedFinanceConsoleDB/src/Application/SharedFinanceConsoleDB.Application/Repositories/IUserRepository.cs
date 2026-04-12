using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsoleDB.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
