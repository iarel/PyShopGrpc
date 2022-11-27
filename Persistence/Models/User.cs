using Persistence.Models.Interfaces;

namespace Persistence.Models;

public class User : Entity
{
    public User(string name, int raiting) : base()
    {
        Name = name;
        Rating = raiting;
    }

    public string Name { get; set; }
    public int Rating { get; set; }
}