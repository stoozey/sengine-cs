using Engine.Common.Classes;
using Engine.Common.Structs;

namespace Engine.Entities.Components;

public class Transform2D : Component
{
    public Vector2 Position;
    public Vector2 Rotation;

    public Transform2D(Entity _owner, Vector2? _position = null, Vector2? _rotation = null) : base(_owner)
    {
        Position = (_position ?? new Vector2());
        Rotation = (_rotation ?? new Vector2());
    }

    public override void Render() { }
    public override void Update() { }
}