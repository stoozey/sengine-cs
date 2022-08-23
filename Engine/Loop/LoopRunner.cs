using System.Numerics;
using Engine.Loop.LoopHandlers;
using Raylib_cs;


namespace Engine.Loop;

public static class Runner
{
    public static bool Running = false;
    private static string windowTitle = "Engine";
    
    public static readonly List<LoopHandler> LoopHandlers = new List<LoopHandler>()
    {
        new EntityLoopHandler(),
        new AudioLoopHandler()
    };
    
    public static string WindowTitle
    {
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            if (Raylib.IsWindowReady())
                Raylib.SetWindowTitle(value);
                
            windowTitle = value;
        }
        get => windowTitle;
    }
    
    private static Vector2 windowSize = new Vector2(1280, 720);
    public static Vector2 WindowSize
    {
        set
        {
            var _value = new Vector2((int) value.X, (int) value.Y);
            
            if (Raylib.IsWindowReady())
                Raylib.SetWindowSize((int) _value.X, (int) _value.Y);

            windowSize = _value;
        }

        get => windowSize;
    }
    
    private static int targetFps = 60;
    public static int TargetFps
    {
        set
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
            
            Raylib.SetTargetFPS(value);
            targetFps = value;
        }

        get => targetFps;
    }

    public static Color BackgroundColour = Color.WHITE;
    
    public static void Initialize()
    {
        Raylib.SetTargetFPS(TargetFps);
        Raylib.InitWindow((int) WindowSize.X, (int) WindowSize.Y, WindowTitle);
        Raylib.InitAudioDevice();
    }
    
    public static void Run()
    {
        Running = true;
        
        while (!Raylib.WindowShouldClose())
        {
            // Update
            foreach (var _loopHandler in LoopHandlers)
                _loopHandler.Update();
            
            // Draw
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BackgroundColour);

            foreach (var _loopHandler in LoopHandlers)
                _loopHandler.Render();
            
            //Raylib.DrawFPS(0, 0);
            
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}