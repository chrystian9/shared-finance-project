using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsole.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsole.Application
{
    public class SharedFinanceAppService
    {
        public readonly IDictionary<Guid, User> _usersById = new Dictionary<Guid, User>();
        public readonly IDictionary<Guid, Account> _accountsByUserId = new Dictionary<Guid, Account>();

        public Guid AddUser(string name)
        {
            var user = new User(name);

            _usersById.Add(user.Id, user);

            return user.Id;
        }

        public Guid AddAccount(Guid userId)
        {
            var account = new Account(userId);

            _accountsByUserId.Add(account.UserId, account);

            return account.Id;
        }

        public void RegisterExpense(Guid payerUserId,
            decimal totalValue,
            string description,
            IEnumerable<TransactionCounterparty> counterparties)
        {
            var payerAccount = _accountsByUserId[payerUserId];

            payerAccount.RegisterExpense(totalValue, description, counterparties);

            foreach (var counterparty in counterparties)
            {
                var counterpartyAccount = _accountsByUserId[counterparty.UserId];

                counterpartyAccount.RegisterTransfer(
                    counterparty.GetValue(totalValue),
                    description,
                    counterparty.UserId
                );
            }
        }
    }
}
