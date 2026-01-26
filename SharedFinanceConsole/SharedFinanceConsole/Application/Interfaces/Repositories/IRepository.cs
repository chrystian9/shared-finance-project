namespace SharedFinanceConsole.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Save(T entity);
    }
}
