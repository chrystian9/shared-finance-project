using SharedFinanceConsole.Application.DataContracts.Responses;
using SharedFinanceConsole.Application.Interfaces.Repositories;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate;
using SharedFinanceConsole.Domain.Aggregates.AccountAggregate.ValueObjects;
using SharedFinanceConsole.Domain.Aggregates.UserAggregate;

namespace SharedFinanceConsole.Application.Services
{
    public class SharedFinanceAppService(IUserRepository userRepository, IAccountRepository accountRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;

        public Guid AddUser(string name)
        {
            var user = new User(name);

            _userRepository.Add(user);

            return user.Id;
        }

        public Guid AddAccount(Guid userId)
        {
            var account = new Account(userId);

            _accountRepository.Add(account);

            return account.Id;
        }

        public void RegisterExpense(Guid payerUserId,
            decimal totalValue,
            string description,
            IEnumerable<TransactionCounterparty> counterparties)
        {
            var payerAccount = _accountRepository.GetById(payerUserId);

            payerAccount.RegisterExpense(totalValue, description, counterparties);

            _accountRepository.Save(payerAccount);

            foreach (var counterparty in counterparties)
            {
                var counterpartyAccount = _accountRepository.GetById(counterparty.UserId);

                counterpartyAccount.RegisterTransfer(
                    counterparty.GetValue(totalValue),
                    description,
                    counterparty.UserId
                );

                _accountRepository.Save(counterpartyAccount);
            }
        }

        public IEnumerable<UserBalanceResponse> GetUsersBalances()
        {
            var users = _userRepository.GetAll();
            var accountsByUserId = _accountRepository.GetAll()
                .ToDictionary(a => a.UserId, a => a);

            var response = new List<UserBalanceResponse>();

            foreach (var user in users)
            {
                if (accountsByUserId.TryGetValue(user.Id, out var account))
                {
                    response.Add(new UserBalanceResponse()
                    {
                        Balance = account.GetBalance(),
                        UserId = user.Id,
                        UserName = user.Name,
                    });
                }
            }

            return response;
        }
    }
}
