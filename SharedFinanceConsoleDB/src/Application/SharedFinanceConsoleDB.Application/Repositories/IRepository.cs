using SharedFinanceConsoleDB.Domain.Common.Entity;
using System.Linq.Expressions;

namespace SharedFinanceConsoleDB.Application.Repositories
{
    public interface IRepository<T> where T : notnull, Entity
    {
        IList<T> GetAll();
        T? GetById(long id);
        T? GetByGuid(Guid guid);
        void Add(T entity);
        void AddAndSaveChanges(T entity);
        void Save(T entity);
        void SaveChanges();
        IList<T> Where(Expression<Func<T, bool>> predicate);
    }
}
