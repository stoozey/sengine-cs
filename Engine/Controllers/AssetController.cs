using System.Runtime.InteropServices;
using System.Security.Cryptography;
using DataPanel.DataTypes;
using Raylib_cs;


namespace Engine.Controllers;

public static class AssetController
{
    private static readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();
    private static readonly Dictionary<Image, Texture2D> Texture2Ds = new Dictionary<Image, Texture2D>();
    
    private static readonly Dictionary<string, Sound> Sounds = new Dictionary<string, Sound>();
    private static readonly Dictionary<string, Music> Music = new Dictionary<string, Music>();

    private static string GetHash(byte[] _bytes)
    {
        using var _md5 = MD5.Create();
        return Convert.ToBase64String( _md5.ComputeHash(_bytes));
    }
    
    public static unsafe Image GetImage(SpriteData _spriteData)
    {
        var _imageData = _spriteData.Image;
        var _hash = GetHash(_imageData);
        var _exists = Images.TryGetValue(_hash, out var _existingImage);
        if (_exists) return _existingImage;
        
        Image _image;
        fixed (byte* _pBuffer = &_imageData[0])
            _image = Raylib.LoadImageFromMemory(_spriteData.Format, (IntPtr) _pBuffer, _imageData.Length);
        
        Images.Add(_hash, _image);
        return _image;
    }

    public static void UnloadImage(Image _image)
    {
        if (Images.ContainsValue(_image))
        {
            var _key = Images.First(_im => Equals(_im.Value, _image)).Key;
            Images.Remove(_key);
        }
        
        Raylib.UnloadImage(_image);
    }

    public static Texture2D GetTexture(SpriteData _spriteData)
    {
        var _image = GetImage(_spriteData);
        var _exists = Texture2Ds.TryGetValue(_image, out var _existingTexture2D);
        if (_exists) return _existingTexture2D;

        var _texture2D = Raylib.LoadTextureFromImage(_image);
        Texture2Ds.Add(_image, _texture2D);
        return _texture2D;
    }
    
    public static void UnloadTexture2D(Texture2D _texture2D)
    {
        if (Texture2Ds.ContainsValue(_texture2D))
        {
            var _key = Texture2Ds.First(_tex => Equals(_tex.Value, _texture2D)).Key;
            Texture2Ds.Remove(_key);
        }
        
        Raylib.UnloadTexture(_texture2D);
    }
    
    public static unsafe Sound GetSound(AudioData _audioData)
    {
        var _soundData = _audioData.Sound;
        var _hash = GetHash(_soundData);
        var _exists = Sounds.TryGetValue(_hash, out var _existingSound);
        if (_exists) return _existingSound;
        
        Sound _sound;
        fixed (byte* _pBuffer = &_soundData[0])
            _sound = Raylib.LoadSoundFromWave(Raylib.LoadWaveFromMemory(_audioData.Format, (IntPtr) _pBuffer, _soundData.Length));
        
        Sounds.Add(_hash, _sound);
        return _sound;
    }

    public static void UnloadSound(Sound _sound)
    {
        if (Sounds.ContainsValue(_sound))
        {
            var _key = Sounds.First(_snd => Equals(_snd.Value, _sound)).Key;
            Sounds.Remove(_key);
        }
        
        Raylib.UnloadSound(_sound);
    }
}