namespace Persistence;

using System.Collections.Concurrent;
using Persistence.Models.Interfaces;

public static class IdGenerator
{
    private static readonly ConcurrentDictionary<Type, int> _dict = new();

    public static void Register(Type type, int startId)
    {
        if(!type.GetInterfaces().Contains(typeof(IEntity)))
            return;
        _dict.TryAdd(type, startId);
            
    }

    public static int NextId(Type type) 
    {
        if(!_dict.TryGetValue(type, out var _))
            return -1;
        return _dict[type] += 1;
    }
}