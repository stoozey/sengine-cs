using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Raylib_cs;

namespace Engine.Controllers;

public static class AssetController
{
    private static readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();
    private static readonly Dictionary<Image, Texture2D> Texture2Ds = new Dictionary<Image, Texture2D>();

    private static string GetHash(byte[] _bytes)
    {
        using var _md5 = MD5.Create();
        return Convert.ToBase64String( _md5.ComputeHash(_bytes));
    }
    
    public static unsafe Image GetImage(byte[] _imageData)
    {
        var _hash = GetHash(_imageData);
        var _exists = Images.TryGetValue(_hash, out var _existingImage);
        if (_exists) return _existingImage;
        
        Image _image;
        fixed (byte* _pBuffer = &_imageData[0])
            _image = Raylib.LoadImageFromMemory(".png", (IntPtr) _pBuffer, _imageData.Length);
        
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

    public static Texture2D GetTexture(byte[] _imageData)
    {
        var _image = GetImage(_imageData);
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
}