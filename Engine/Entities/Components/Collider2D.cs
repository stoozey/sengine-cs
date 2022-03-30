using Engine.Common.Structs;
using Raylib_cs;
using Vector2 = System.Numerics.Vector2;

namespace Engine.Entities.Components;

public class Collider2D : Component
{
    public Rectangle Rectangle;

    public string CollisionGroup;
    
    public Collider2D(Entity _owner, string _collisionGroup = EntityManager.COLLISION_GROUP_DEFAULT, Rectangle _rectangle = new Rectangle()) : base(_owner)
    {
        CollisionGroup = _collisionGroup;
        Rectangle = _rectangle;
    }
    
    public Entity? GetCollidingEntity()
    {
        var _position = new Vector2((int) Rectangle.x, (int) Rectangle.y);
        return (from _entity in EntityManager.GetCollisionGroup2D(CollisionGroup) 
            let _collider2D = _entity.GetComponent<Collider2D>()
            where Raylib.CheckCollisionPointRec(_position, _collider2D.Rectangle) select _entity).FirstOrDefault();
    }
    
    public void CollideWith<T>() where T : Entity
    {
        var _entities = EntityManager.GetEntitiesOfType<T>();

        foreach (var _entity in _entities)
        {
            var _collider2D = _entity.GetComponent<Collider2D>();
            var _rectangle = _collider2D.Rectangle;
            var _collisionRect = Raylib.GetCollisionRec(Rectangle, _rectangle);
            Console.WriteLine(_collisionRect);
        }
    }
    
    public override void Render()
    {
        Raylib.DrawRectangleLines((int) Rectangle.x, (int) Rectangle.y, (int) Rectangle.width, (int) Rectangle.height, Color.RED);
    }

    public override void Update()
    {
    }
}