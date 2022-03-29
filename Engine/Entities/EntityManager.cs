namespace Engine.Entities;

public static class EntityManager
{
    private static int entityId = 0;
    public static readonly List<Entity> Entities = new List<Entity>();

    public static Entity? GetEntity(int _entityId)
        => Entities.Find(_entity => _entity.Id == _entityId);
    
    public static void ResetEntityId()
        => entityId = 0;
    
    public static int IncrementEntityId()
        => entityId++;
}