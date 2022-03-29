using System.Numerics;
using Raylib_cs;

namespace Engine.Entities.Components;

public class Sprite2D : Component
{
    private readonly Transform transform;
    private Texture2D? texture2D;

    public Color Color = Color.WHITE;
    public float Scale = 1.0f;
    
    public void SetTexture(Texture2D _texture2D)
    {
        texture2D = _texture2D;
    }
    
    public override void Render()
    {
        if (texture2D == null) return;
        
        var _position = transform.Position;
        Raylib.DrawTextureEx((Texture2D) texture2D, new Vector2(_position.X, _position.Y), transform.Rotation.Y, Scale, Color);
    }

    public override void Update() { }

    public Sprite2D(Entity _owner) : base(_owner)
    {
        transform = Require<Transform>();
    }
}