using SharedFinanceConsoleDB.Application.Abstractions;
using SharedFinanceConsoleDB.Application.DataContracts.Responses;

namespace SharedFinanceConsoleDB.Application.Queries.GetUsersBalances
{
    public class GetUsersBalancesQuery : IRequest<IEnumerable<UserBalanceResponse>> { }
}
