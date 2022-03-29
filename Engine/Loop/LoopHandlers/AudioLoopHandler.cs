using TempoSurge.Controllers;

namespace Engine.Loop.LoopHandlers;

public class AudioLoopHandler : LoopHandler
{
    public override void Update()
    {
        MusicController.UpdateMusicStream();
    }

    public override void Render() { }
}