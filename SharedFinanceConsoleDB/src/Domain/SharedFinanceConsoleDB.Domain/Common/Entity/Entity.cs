namespace SharedFinanceConsoleDB.Domain.Common.Entity
{
    public abstract class Entity
    {
        public long Id { get; protected set; }
        public Guid Guid { get; protected set; }

        public Entity()
        {
            Guid = Guid.NewGuid();
        }
    }
}
