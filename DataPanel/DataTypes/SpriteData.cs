using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel.DataTypes;


[MessagePackObject]
public struct SpriteData : IPanelData
{
    [Key(0)] public string Name { get; }
    [Key(1)] public string Format { get; }
    [Key(2)] public int Width { get; }
    [Key(3)] public int Height { get; }
    [Key(4)] public byte[] Image { get; }

    public SpriteData(string _name, string _format, int _width, int _height, byte[] _image)
    {
        Name = _name;
        Format = _format;
        Width = _width;
        Height = _height;
        Image = _image;
    }
}