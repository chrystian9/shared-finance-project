using SharedFinanceConsole.Application.Abstractions;
using SharedFinanceConsole.Application.DataContracts.Responses;

namespace SharedFinanceConsole.Application.Queries
{
    public class GetUsersBalancesQuery : IRequest<IEnumerable<UserBalanceResponse>> { }
}
