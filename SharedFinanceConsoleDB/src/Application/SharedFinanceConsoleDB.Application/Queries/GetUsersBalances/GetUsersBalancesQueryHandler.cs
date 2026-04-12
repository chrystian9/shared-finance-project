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
            var accountsByUserId = accountRepository.GetAll()
                .ToDictionary(a => a.UserId, a => a);

            var response = new List<UserBalanceResponse>();

            foreach (var user in users)
            {
                if (accountsByUserId.TryGetValue(user.Id, out var account))
                {
                    response.Add(new UserBalanceResponse()
                    {
                        Balance = account.GetBalance(),
                        UserId = user.Guid,
                        UserName = user.Name,
                    });
                }
            }

            return response;
        }
    }
}
