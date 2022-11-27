using Contracts.Requests;
using Contracts.Responses;

namespace Application.AppServices;

public interface IBillingAppService
{
    public Task<IEnumerable<UserResponse>> ListUsers();
    public Task<Response> MoveCoins(TransactionRequest transaction);
    public Task<CoinResponse> LongestHistoryCoin();
    public Task<Response> CoinsEmission(long amount);
}