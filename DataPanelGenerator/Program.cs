using System.Drawing;
using DataPanel;
using DataPanel.DataTypes;
using DataPanelGenerator.Common.Helper;

switch (args[0])
{
    case "SpriteData":
    {
        var _path = args[1];
        
        using var _dataPanel = new DataPanel<SpriteData>();
        var _files = Directory.EnumerateFiles(_path, "*.*", SearchOption.AllDirectories)
            .Where(_file => _file.EndsWith(".png"));
        
        foreach (var _file in _files)
        {
            string _name = IOHelper.GetPanelDataName(Path.GetFileNameWithoutExtension(_file));
            string _format = Path.GetExtension(_file);
            Size _size = ImageHelper.GetDimensions(_file);
            byte[] _imageData = File.ReadAllBytes(_file);
            
            _dataPanel.AddData(new SpriteData(_name, _format, _size.Width, _size.Height, _imageData));
        }

        var _panelName = Path.GetFileName(_path);
        var _filename = $"{_path}/{_panelName}.dp";
        foreach (var _key in _dataPanel.GetKeys())
            Console.WriteLine(_key);
        Console.WriteLine(_filename);
        _dataPanel.ToFile(_filename);
        break;
    }
    
    case "AudioData":
    {
        var _path = args[1];

        using var _dataPanel = new DataPanel<AudioData>();
        var _files = Directory.EnumerateFiles(_path, "*.*", SearchOption.AllDirectories)
            .Where(_file => _file.EndsWith(".mp3") || _file.EndsWith(".ogg") || _file.EndsWith(".wav"));
        
        foreach (var _file in _files)
        {
            string _name = Path.GetFileNameWithoutExtension(_file);
            string _format = Path.GetExtension(_file);
            byte[] _audioData = File.ReadAllBytes(_file);
            
            _dataPanel.AddData(new AudioData(_name, _format, _audioData));
        }

        var _panelName = Path.GetFileName(_path);
        var _filename = $"{_path}/{_panelName}.dp";
        Console.WriteLine(_filename);
        _dataPanel.ToFile(_filename);
        break;
    }
}

Console.ReadLine();