using DataPanel.DataTypes;
using Raylib_cs;

namespace Engine.Controllers;

public static class MusicController
{
    private static Music? music;

    public static float MusicPosition => ((music == null) ? 0.0f : Raylib.GetMusicTimePlayed((Music) music));

    public static unsafe void SetMusic(AudioData _audioData)
    {
        if (music != null)
        {
            Stop();
            Raylib.UnloadMusicStream((Music) music);
        }

        var _soundData = _audioData.Sound;
        fixed (byte* _pBuffer = &_soundData[0])
            music = Raylib.LoadMusicStreamFromMemory(_audioData.Format, (IntPtr) _pBuffer, _soundData.Length);
    }

    public static void SetMusicPosition(float _musicPosition)
    {
        if (music == null) return;
        
        //Raylib.SeekMusicStream((Music) music, _musicPosition);
    }
    
    public static void UpdateMusicStream()
    {
        if (music == null) return;
        
        Raylib.UpdateMusicStream((Music) music);
    }
    
    public static void Play()
    {
        if (music == null) return;
        
        Raylib.PlayMusicStream((Music) music);
    }

    public static void Pause()
    {
        if (music == null) return;
        
        Raylib.PauseMusicStream((Music) music);
    }    
    
    public static void Stop()
    {
        if (music == null) return;
        
        Raylib.StopMusicStream((Music) music);
    }
}