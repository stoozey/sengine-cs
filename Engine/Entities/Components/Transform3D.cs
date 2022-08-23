using Engine.Common.Classes;
using Engine.Common.Structs;

namespace Engine.Entities.Components;

public class Transform3D : Component
{
    public Vector3 Position;
    public Vector3 Rotation;

    public Transform3D(Entity _owner, Vector3? _position = null, Vector3? _rotation = null) : base(_owner)
    {
        if (_position != null)
            Position = (Vector3) _position;
                
        if (_rotation != null)
            Rotation = (Vector3) _rotation;
    }

    public override void Render() { }
    public override void Update() { }
}