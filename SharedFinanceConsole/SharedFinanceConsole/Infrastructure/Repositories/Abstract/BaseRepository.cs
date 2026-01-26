using SharedFinanceConsole.Application.Interfaces.Repositories;

namespace SharedFinanceConsole.Infrastructure.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected readonly Dictionary<Guid, T> _store = [];

        protected abstract Guid GetEntityId(T entity);

        public virtual IList<T> GetAll()
        {
            return [.. _store.Values];
        }

        public virtual void Add(T entity)
        {
            var id = GetEntityId(entity);
            _store.Add(id, entity);
        }

        public virtual T GetById(Guid id)
        {
            return _store[id];
        }

        public virtual void Save(T entity)
        {
            var id = GetEntityId(entity);
            _store[id] = entity;
        }
    }
}
