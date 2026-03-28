using SharedFinanceConsoleDB.Domain.Common.DomainException;
using SharedFinanceConsoleDB.Domain.Common.Entity;

namespace SharedFinanceConsoleDB.Domain.Aggregates.UserAggregate
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
