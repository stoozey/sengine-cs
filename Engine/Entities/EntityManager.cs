using System.Numerics;
using Engine.Entities.Components;
using Raylib_cs;

namespace Engine.Entities;

public static class EntityManager
{
    public const string COLLISION_GROUP_DEFAULT = "default";
    
    public static readonly List<Entity> Entities = new List<Entity>();

    private static int entityId = 0;

    public static Entity? GetEntity(int _entityId)
        => Entities.Find(_entity => _entity.Id == _entityId);

    public static T[] GetEntitiesOfType<T>() where T : class
        => Entities.FindAll(_entity => _entity.GetType() == typeof(T)).ToArray() as T[] ?? Array.Empty<T>();

    public static Entity[] GetEntitiesWithComponent<T>() where T : class
        => Entities.FindAll(_entity => _entity.HasComponent<T>()).ToArray();

    public static Entity[] GetCollisionGroup2D(string _collisionGroup)
    {
        var _entities = GetEntitiesWithComponent<Collider2D>().ToList();
        return _entities.FindAll(_entity => _entity.GetComponent<Collider2D>().CollisionGroup == _collisionGroup).ToArray();
    }

    public static Entity? GetCollidingEntity(Entity _sender, Vector2 _position, string _collisionGroup = COLLISION_GROUP_DEFAULT)
    {
        return (from _entity in GetCollisionGroup2D(_collisionGroup) let _collider2D = _entity.GetComponent<Collider2D>()
            where _entity != _sender && Raylib.CheckCollisionPointRec(_position, _collider2D.Rectangle) select _entity).FirstOrDefault();
    }
    
    public static Entity? GetCollidingEntity(Entity _sender, Rectangle _rectangle, string _collisionGroup = COLLISION_GROUP_DEFAULT)
    {
        return (from _entity in GetCollisionGroup2D(_collisionGroup) let _collider2D = _entity.GetComponent<Collider2D>()
            where _entity != _sender && Raylib.CheckCollisionRecs(_rectangle, _collider2D.Rectangle) select _entity).FirstOrDefault();
    }
    
    public static void ResetEntityId()
        => entityId = 0;
    
    public static int IncrementEntityId()
        => entityId++;
}