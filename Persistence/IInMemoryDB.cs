using Persistence.Models;

namespace Persistence;

public interface IInMemoryDb
{
    public List<User> Users { get; }
    public List<Coin> Coins { get; }
    public List<Transaction> Transactions { get; }
}