using Persistence.Models.Interfaces;

namespace Persistence.Models;

public abstract class Entity : IEntity
{
    private readonly int _id;

    public Entity()
    {
        _id = IdGenerator.NextId(this.GetType());
    }

    public int Id => _id;
}