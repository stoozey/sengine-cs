using System.Numerics;
using DataPanel.DataTypes;
using Engine.Controllers;
using Raylib_cs;


namespace Engine.Entities.Components;

public enum XAlign
{
    Left,
    Center,
    Right
}

public enum YAlign
{
    Top,
    Middle,
    Bottom
}

public class Sprite2D : Component
{
    private readonly Transform2D transform;
    private Texture2D? texture;

    public Color Color = Color.WHITE;
    public float Scale = 1.0f;
    public int Width = 0;
    public int Height = 0;

    public XAlign XAlign = XAlign.Center;
    public YAlign YAlign = YAlign.Middle;
    private readonly Rectangle rectangle = new Rectangle();

    public Rectangle Rectangle
    {
        get
        {
            var _position = transform.Position;
            return new Rectangle(_position.X, _position.Y, Width, Height);
        }
    }
    
    public void SetSprite(SpriteData _spriteData)
    {
        var _texture = AssetController.GetTexture(_spriteData);
        texture = _texture;
        Width = _spriteData.Width;
        Height = _spriteData.Height;
    }
    
    public override void Render()
    {
        if (texture == null) return;
        
        var _position = transform.Position;
        Raylib.DrawTextureEx((Texture2D) texture, new Vector2(_position.X, _position.Y), transform.Rotation.Y, Scale, Color);
    }

    public override void Update()
    {
    }

    public Sprite2D(Entity _owner) : base(_owner)
    {
        transform = Require<Transform2D>();
    }
}