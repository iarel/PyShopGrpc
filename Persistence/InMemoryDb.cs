using Persistence.Models;
using Persistence.Models.Interfaces;
namespace Persistence;

public class InMemoryDb : IInMemoryDb
{
    private readonly List<User> _users;
    private readonly List<Coin> _coins;
    private readonly List<Transaction> _transactions;

    public InMemoryDb()
    {
        RegEntities();

        _users = new List<User>()
        {
            new User ("boris", 5000 ),
            new User ("maria", 1000 ),
            new User ("oleg",  800 ),
        };
        _coins = new ();
        _transactions = new ();
    }

    public List<User> Users => _users;
    public List<Coin> Coins => _coins;
    public List<Transaction> Transactions => _transactions;

    private void RegEntities()
    {
        IdGenerator.Register(typeof(Coin), 0);
        IdGenerator.Register(typeof(User), 0);
        IdGenerator.Register(typeof(Transaction), 0);
    }

    private class IdComparer<T> : IComparer<T> where T : IEntity
    {
        public int Compare(T x, T y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}
