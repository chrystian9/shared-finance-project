using SharedFinanceConsole.Domain.Common.DomainException;
using SharedFinanceConsole.Domain.Common.Entity;

namespace SharedFinanceConsole.Domain.Aggregates.UserAggregate
{
    public class User : Entity
    {
        public string Name { get; init; }

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(DomainException.UserNameEmpty);

            Name = name;
        }
    }
}
