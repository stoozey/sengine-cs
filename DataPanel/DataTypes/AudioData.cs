using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel.DataTypes;


[MessagePackObject]
public struct AudioData : IPanelData
{
    [Key(0)] public string Name { get; }
    [Key(3)] public byte[] Sound { get; }

    public AudioData(string _name, byte[] _sound)
    {
        Name = _name;
        Sound = _sound;
    }
}