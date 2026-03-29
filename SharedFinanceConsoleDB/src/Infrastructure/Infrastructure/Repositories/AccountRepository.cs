using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsoleDB.Infrastructure.Repositories.Abstract;

namespace SharedFinanceConsoleDB.Infrastructure.Repositories
{
    public class AccountRepository(DbContext _context) : BaseRepository<Account>(_context), IAccountRepository
    {
    }
}
