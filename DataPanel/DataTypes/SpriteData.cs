using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel.DataTypes;


[MessagePackObject]
public struct SpriteData : IPanelData
{
    [Key(0)] public string Name { get; }
    [Key(1)] public int Width { get; }
    [Key(2)] public int Height { get; }
    [Key(3)] public byte[] Image { get; }

    public SpriteData(string _name, int _width, int _height, byte[] _image)
    {
        Name = _name;
        Width = _width;
        Height = _height;
        Image = _image;
    }
}