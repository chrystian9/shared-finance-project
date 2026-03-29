using Microsoft.EntityFrameworkCore;
using SharedFinanceConsoleDB.Application.Repositories;
using SharedFinanceConsoleDB.Domain.Common.Entity;

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

        public virtual T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual void Save(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
