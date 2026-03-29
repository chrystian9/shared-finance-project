using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Application.Repositories
{
    public interface IRepository<T> where T : notnull, Entity
    {
        IList<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Save(T entity);
    }
}
