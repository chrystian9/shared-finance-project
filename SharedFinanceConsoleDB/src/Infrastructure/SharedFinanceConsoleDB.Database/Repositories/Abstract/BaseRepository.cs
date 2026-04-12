using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Domain.Common.Entity;
using System.Linq.Expressions;

namespace SharedFinanceConsoleDB.Infrastructure.Repositories.Abstract
{
    public abstract class BaseRepository<T>(DbContext _context) : IRepository<T> where T : notnull, Entity
    {
        public virtual IList<T> GetAll()
        {
            return [.. _context.Set<T>()];
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual T? GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual T? GetByGuid(Guid guid)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Guid == guid);
        }

        public virtual IList<T> Where(Expression<Func<T, bool>> predicate)
        {
            return [.. _context.Set<T>().Where(predicate)];
        }
    }
}
