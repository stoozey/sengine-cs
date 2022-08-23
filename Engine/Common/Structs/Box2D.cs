using Engine.Common.Structs;

namespace Engine.Common.Structs;

public class Box2D
{
    public Vector2 Min;
    public Vector2 Max;

    public int Left => Min.X;
    public int Right => Max.X;
    public int Top => Min.Y;
    public int Bottom => Max.Y;

    public Box2D(Vector2 _min, Vector2 _max)
    {
        Min = _min;
        Max = _max;
    }
}