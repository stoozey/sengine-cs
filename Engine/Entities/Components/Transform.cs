using System.Numerics;

namespace Engine.Entities.Components;

public class Transform : Component
{
    public Vector3 Position = Vector3.Zero;
    public Vector3 Rotation = Vector3.Zero;

    public Transform(Entity _owner, Vector3? _position = null, Vector3? _rotation = null) : base(_owner)
    {
        if (_position != null)
            Position = (Vector3) _position;
                
        if (_rotation != null)
            Rotation = (Vector3) _rotation;
    }

    public override void Render() { }
    public override void Update() { }
}