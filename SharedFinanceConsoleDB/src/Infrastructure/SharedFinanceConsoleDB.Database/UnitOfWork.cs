using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Application.Abstractions;

namespace SharedFinanceConsoleDB.Database
{
    public class UnitOfWork(DbContext _context) : IUnitOfWork
    {
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
