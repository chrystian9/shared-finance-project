using SharedFinanceConsole.Application.Abstractions;
using SharedFinanceConsole.Application.DataContracts.Responses;
using SharedFinanceConsole.Application.Queries;
using SharedFinanceConsole.Application.Repositories;

namespace SharedFinanceConsole.Application.Handlers.Queries
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
                        UserId = user.Id,
                        UserName = user.Name,
                    });
                }
            }

            return response;
        }
    }
}
