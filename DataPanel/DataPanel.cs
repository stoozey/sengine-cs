using System.Collections.Concurrent;
using System.IO.Compression;
using DataPanel.Interfaces;
using MessagePack;

namespace DataPanel;

public class DataPanel<T> where T: IPanelData
{
    public const string FILE_EXTENSION = ".tss";
    
    protected readonly ObservableConcurrentDictionary<string, T> Data;
    
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
        var _uncompressedStream = new MemoryStream();
        using BinaryWriter _binaryWriter = new BinaryWriter(_uncompressedStream);
        T[] _dataArray = new T[Data.Count()];
        Data.Values.CopyTo(_dataArray, 0);
        _binaryWriter.Write(MessagePackSerializer.Serialize(_dataArray));
        
        var _compressedStream = new MemoryStream();
        using var _compressor = new GZipStream(_compressedStream, CompressionMode.Compress);
        _uncompressedStream.Position = 0;
        _uncompressedStream.CopyTo(_compressor);
        _compressor.Close();
        _binaryWriter.Close();
        
        File.WriteAllBytes( _filename, _compressedStream.ToArray());
    }

    public DataPanel(string? _filename = null)
    {
        Data = new ObservableConcurrentDictionary<string, T>();
        if (_filename == null) return;
        
        using FileStream _compressedStream = File.Open(_filename, FileMode.Open);
        using var _decompressor = new GZipStream(_compressedStream, CompressionMode.Decompress);

        var _decompressedStream = new MemoryStream();
        _decompressor.CopyTo(_decompressedStream);
        _decompressedStream.Position = 0;

        using var _binaryReader = new BinaryReader(_decompressedStream);
        var _dataArray = MessagePackSerializer.Deserialize<T[]>(_decompressedStream.ToArray());
        foreach (var _data in _dataArray)
            Data.Add(_data.Name, _data);
    }
}