using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel.DataTypes;


[MessagePackObject]
public struct SpriteSheetData : IPanelData
{
    public SpriteSheetData(string _name, int _widthPerSprite, int _heightPerSprite, SpriteData _spriteData)
    {
        Name = _name;
        WidthPerSprite = _widthPerSprite;
        HeightPerSprite = _heightPerSprite;
        SpriteData = _spriteData;
    }

    [Key(0)] public string Name { get; }
    [Key(1)] public int WidthPerSprite { get; }
    [Key(2)] public int HeightPerSprite { get; }
    [Key(3)] public SpriteData SpriteData { get; }
}