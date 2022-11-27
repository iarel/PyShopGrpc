using Persistence.Models.Interfaces;

namespace Persistence.Models;

public class Coin : Entity
{
    public Coin(int userId) : base()
    {
        UserId = userId;
    }

    public int UserId { get; set; }
}