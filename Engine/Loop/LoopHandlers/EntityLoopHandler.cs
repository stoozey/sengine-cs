using Engine.Entities;

namespace Engine.Loop.LoopHandlers;

public class EntityLoopHandler : LoopHandler
{
    public override void Update()
    {
        foreach (var _entity in EntityManager.Entities.ToArray())
        {
            _entity.Collide();
            _entity.Update();
            
            if (_entity.AutoManageComponents)
                _entity.UpdateComponents();
        }
    }
    
    public override void Render()
    {
        foreach (var _entity in EntityManager.Entities.ToArray())
        {
            _entity.Render();
            
            if (_entity.AutoManageComponents)
                _entity.RenderComponents();
        }
    }
}