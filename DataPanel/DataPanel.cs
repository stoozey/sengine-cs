using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel;

public class DataPanel<T> : IDisposable where T: IPanelData
{
    private readonly Dictionary<string, T> Data;

    public string[] GetKeys()
        => Data.Keys.ToArray();
    
    public T GetData(string _name)
    {
        return Data[_name];
    }
    
    public void AddData(T _data)
    {
        Data.Add(_data.Name, _data);
    }

    public bool RemoveData(string _name)
    {
        return Data.Remove(_name);
    }

    public void ToFile(string _filename)
    {
        T[] _dataArray = new T[Data.Count];
        Data.Values.CopyTo(_dataArray, 0);
        
        var _lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var _panelData = MessagePackSerializer.Serialize(_dataArray, _lz4Options);
        File.WriteAllBytes( _filename, _panelData);
    }

    public DataPanel(string? _filename = null)
    {
        Data = new Dictionary<string, T>();
        if (_filename == null) return;
        
        var _lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var _panelData = new ReadOnlyMemory<byte>( File.ReadAllBytes(_filename));
        var _dataArray = MessagePackSerializer.Deserialize<T[]>(_panelData, _lz4Options);
        foreach (var _data in _dataArray)
            Data.Add(_data.Name, _data);
    }

    public void Dispose()
    {
        GC.Collect();
    }
}