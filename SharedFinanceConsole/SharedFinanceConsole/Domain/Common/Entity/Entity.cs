namespace SharedFinanceConsole.Domain.Common.Entity
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
