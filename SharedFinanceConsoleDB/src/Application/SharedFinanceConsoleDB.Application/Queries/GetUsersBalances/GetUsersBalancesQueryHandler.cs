using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.DataContracts.Responses;
using SharedFinanceConsoleDB.Application.Repositories;

namespace SharedFinanceConsoleDB.Application.Queries.GetUsersBalances
{
    public class GetUsersBalancesQueryHandler(IUserRepository userRepository, IAccountRepository accountRepository)
        : IRequestHandler<GetUsersBalancesQuery, IEnumerable<UserBalanceResponse>>
    {
        public IEnumerable<UserBalanceResponse> Handle(GetUsersBalancesQuery request)
        {
            var users = userRepository.GetAll();
            var accounts = accountRepository.GetAll();

            return users
                .Join(accounts,
                    u => u.Guid,
                    a => a.User.Guid,
                    (user, account) => new UserBalanceResponse()
                    {
                        Balance = account.GetBalance(),
                        UserId = user.Guid,
                        UserName = user.Name,
                    })
                .OrderBy(r => r.UserName);
        }
    }
}
