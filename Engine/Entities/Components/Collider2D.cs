using Engine.Common.Structs;
using Raylib_cs;

namespace Engine.Entities.Components;

public class Collider2D : Component
{
    public Rectangle Rectangle;
    
    public Collider2D(Entity _owner, Rectangle _rectangle = new Rectangle()) : base(_owner)
    {
        Rectangle = _rectangle;
    }

    public void CollideWith<T>() where T : Entity
    {
        var _entities = EntityManager.GetEntitiesOfType<T>();
        if (_entities == null) return;

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