using Contracts.Responses;
using Contracts.Enums;
using Contracts.Requests;
using Persistence.Models;
using Persistence;

namespace Application.AppServices;

public class BillingAppService : IBillingAppService
{
    private const string separator = "-";
    private readonly IInMemoryDb _db;
    public BillingAppService(IInMemoryDb inMemoryDb)
    {
        _db = inMemoryDb;
    }

    public async Task<Response> CoinsEmission(long amount)
    {
        int counter = 0;
        double oneCoinPrice = _db.Users.Sum(user => user.Rating)/amount;

        foreach(var user in _db.Users)
        {
            var coinsCount = (int)Math.Floor(user.Rating/oneCoinPrice);
            coinsCount = coinsCount > 0 ? coinsCount : 1;
            for(int i = 0; i < coinsCount; i++)
            {
                _db.Coins.Add(new Coin(user.Id));
                counter++;
            }
        }

        return await Task.FromResult(new Response(TransactionStatus.Ok, $"{counter} coins was added"));
    }

    public async Task<IEnumerable<UserResponse>> ListUsers()
    {
        var users = _db.Users
                .Select(user => new UserResponse(user.Name, _db.Coins.Where(coin => coin.UserId == user.Id).Count()));

        return await Task.FromResult(users);
    }

    public async Task<CoinResponse> LongestHistoryCoin()
    {
        var trHistory = _db.Transactions.GroupBy(tr => tr.CoinId).MaxBy(gr => gr.Count());
        if(trHistory is null)
            return null;

        var firstSourceName = _db.Users.First(user => trHistory.First().SourceId == user.Id).Name;

        var history = $"{firstSourceName}{separator}" +
            string.Join(separator, trHistory.Select(t => _db.Users.First(user => user.Id == t.DestinationId).Name));

        var coindId = trHistory.First().CoinId;
        return await Task.FromResult(new CoinResponse(coindId, history));
    }

    public async Task<Response> MoveCoins(TransactionRequest transaction)
    {
        var usersByNames = _db.Users
            .Where(user => user.Name == transaction.Source || user.Name == transaction.Destination)
            .GroupBy(key => key.Name)
            .ToDictionary(g => g.Key, g => g.FirstOrDefault());

        if(!(usersByNames.TryGetValue(transaction.Destination, out var dst) && 
             usersByNames.TryGetValue(transaction.Source, out var src)))
            return null;
        
        var coins = _db.Coins.Where(coin => coin.UserId == src.Id);
        if(coins.Count() < transaction.Amount)
            return null;

        foreach(var coin in coins.Take((int)transaction.Amount))
        {
            coin.UserId = dst.Id;        
            _db.Transactions.Add(new Transaction(src.Id, dst.Id, coin.Id));
        }

        return await Task.FromResult(new Response(TransactionStatus.Ok, "Ok"));
    }
}