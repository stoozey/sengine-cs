using Engine.Levels;

namespace Engine.Controllers;

public static class LevelController
{
    public static Level Level;
    
    public static void SetLevel(Level _level)
    {
        Level = _level;
        _level.OnStart();
    }
}