namespace Engine.Entities;

public static class EntityManager
{
    private static int entityId = 0;
    public static readonly List<Entity> Entities = new List<Entity>();

    public static Entity? GetEntity(int _entityId)
        => Entities.Find(_entity => _entity.Id == _entityId);

    public static T[]? GetEntitiesOfType<T>() where T : class
        => Entities.FindAll(_entity => _entity.GetType() == typeof(T)).ToArray() as T[];
    

    public static void ResetEntityId()
        => entityId = 0;
    
    public static int IncrementEntityId()
        => entityId++;
}