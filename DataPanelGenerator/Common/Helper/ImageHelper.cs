using System.Drawing;

namespace DataPanelGenerator.Common.Helper;

public static class ImageHelper
{
    private const string ERROR_MESSAGE = "Could not recognize image format.";

    private static readonly Dictionary<byte[], Func<BinaryReader, Size>> ImageFormatDecoders = new Dictionary<byte[], Func<BinaryReader, Size>>()
    {
        { new byte[]{ 0x42, 0x4D }, DecodeBitmap},
        { new byte[]{ 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }, DecodeGif },
        { new byte[]{ 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }, DecodeGif },
        { new byte[]{ 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, DecodePng },
        { new byte[]{ 0xff, 0xd8 }, DecodeJfif },
    };

    /// <summary>
    /// Gets the dimensions of an image.
    /// </summary>
    /// <param name="_path">The path of the image to get the dimensions of.</param>
    /// <returns>The dimensions of the specified image.</returns>
    /// <exception cref="ArgumentException">The image was of an unrecognized format.</exception>
    public static Size GetDimensions(string _path)
    {
        using BinaryReader _binaryReader = new BinaryReader(File.OpenRead(_path));
        try
        {
            return GetDimensions(_binaryReader);
        }
        catch (ArgumentException _e)
        {
            if (_e.Message.StartsWith(ERROR_MESSAGE))
            {
                throw new ArgumentException(ERROR_MESSAGE, nameof(_path), _e);
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Gets the dimensions of an image.
    /// </summary>
    /// <param name="path">The path of the image to get the dimensions of.</param>
    /// <param name="_binaryReader"></param>
    /// <returns>The dimensions of the specified image.</returns>
    /// <exception cref="ArgumentException">The image was of an unrecognized format.</exception>    
    public static Size GetDimensions(BinaryReader _binaryReader)
    {
        int _maxMagicBytesLength = ImageFormatDecoders.Keys.OrderByDescending(_x => _x.Length).First().Length;

        byte[] _magicBytes = new byte[_maxMagicBytesLength];

        for (int _i = 0; _i < _maxMagicBytesLength; _i += 1)
        {
            _magicBytes[_i] = _binaryReader.ReadByte();

            foreach(var _kvPair in ImageFormatDecoders)
            {
                if (_magicBytes.StartsWith(_kvPair.Key))
                {
                    return _kvPair.Value(_binaryReader);
                }
            }
        }

        throw new ArgumentException(ERROR_MESSAGE, "_binaryReader");
    }

    private static bool StartsWith(this byte[] _thisBytes, byte[] _thatBytes)
    {
        for(int _i = 0; _i < _thatBytes.Length; _i+= 1)
        {
            if (_thisBytes[_i] != _thatBytes[_i])
            {
                return false;
            }
        }
        return true;
    }

    private static short ReadLittleEndianInt16(this BinaryReader _binaryReader)
    {
        byte[] _bytes = new byte[sizeof(short)];
        for (int _i = 0; _i < sizeof(short); _i += 1)
        {
            _bytes[sizeof(short) - 1 - _i] = _binaryReader.ReadByte();
        }
        return BitConverter.ToInt16(_bytes, 0);
    }

    private static int ReadLittleEndianInt32(this BinaryReader _binaryReader)
    {
        byte[] _bytes = new byte[sizeof(int)];
        for (int _i = 0; _i < sizeof(int); _i += 1)
        {
            _bytes[sizeof(int) - 1 - _i] = _binaryReader.ReadByte();
        }
        return BitConverter.ToInt32(_bytes, 0);
    }

    private static Size DecodeBitmap(BinaryReader _binaryReader)
    {
        _binaryReader.ReadBytes(16);
        int _width = _binaryReader.ReadInt32();
        int _height = _binaryReader.ReadInt32();
        return new Size(_width, _height);
    }

    private static Size DecodeGif(BinaryReader _binaryReader)
    {
        int _width = _binaryReader.ReadInt16();
        int _height = _binaryReader.ReadInt16();
        return new Size(_width, _height);
    }

    private static Size DecodePng(BinaryReader _binaryReader)
    {
        _binaryReader.ReadBytes(8);
        int _width = _binaryReader.ReadLittleEndianInt32();
        int _height = _binaryReader.ReadLittleEndianInt32();
        return new Size(_width, _height);
    }

    private static Size DecodeJfif(BinaryReader _binaryReader)
    {
        while (_binaryReader.ReadByte() == 0xff)
        {
            byte _marker = _binaryReader.ReadByte();
            short _chunkLength = _binaryReader.ReadLittleEndianInt16();

            if (_marker == 0xc0)
            {
                _binaryReader.ReadByte();

                int _height = _binaryReader.ReadLittleEndianInt16();
                int _width = _binaryReader.ReadLittleEndianInt16();
                return new Size(_width, _height);
            }

            _binaryReader.ReadBytes(_chunkLength - 2);
        }

        throw new ArgumentException(ERROR_MESSAGE);
    }
}