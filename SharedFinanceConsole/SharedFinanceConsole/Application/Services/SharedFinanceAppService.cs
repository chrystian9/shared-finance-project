using SharedFinanceConsole.Application.DataContracts.Responses;
using SharedFinanceConsole.Application.Repositories;

namespace SharedFinanceConsole.Application.Services
{
    public class SharedFinanceAppService(IUserRepository userRepository, IAccountRepository accountRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;

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
