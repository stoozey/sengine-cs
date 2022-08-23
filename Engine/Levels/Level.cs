using Raylib_cs;

namespace Engine.Levels;

public abstract class Level
{
    public Sound Music;
    public abstract void OnStart();
}