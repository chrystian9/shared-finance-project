using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate;
using SharedFinanceConsoleDB.Infrastructure.Repositories.Abstract;

namespace SharedFinanceConsoleDB.Infrastructure.Repositories
{
    public class UserRepository(DbContext _context) : BaseRepository<User>(_context), IUserRepository
    {
    }
}
