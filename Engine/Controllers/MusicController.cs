using Raylib_cs;

namespace Engine.Controllers;

public static class MusicController
{
    private static Music? music;

    public static float MusicPosition => ((music == null) ? 0.0f : Raylib.GetMusicTimePlayed((Music) music));

    public static void SetMusic(string _filename)
    {
        if (music != null)
        {
            Stop();
            Raylib.UnloadMusicStream((Music) music);
        }
        
        music = Raylib.LoadMusicStream(_filename);
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