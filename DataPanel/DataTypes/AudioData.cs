using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel.DataTypes;


[MessagePackObject]
public struct AudioData : IPanelData
{
    [Key(0)] public string Name { get; }
    [Key(1)] public string Format { get; }
    [Key(2)] public byte[] Sound { get; }

    public AudioData(string _name, string _format, byte[] _sound)
    {
        Name = _name;
        Format = _format;
        Sound = _sound;
    }
}