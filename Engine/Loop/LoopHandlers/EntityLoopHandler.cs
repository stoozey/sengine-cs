using Engine.Entities;
using Engine.Entities.Components;

namespace Engine.Loop.LoopHandlers;

public class EntityLoopHandler : LoopHandler
{
    public override void Update()
    {
        foreach (var _entity in EntityManager.Entities.ToArray())
        {
            if (_entity.AutoManageComponents)
                _entity.UpdateComponents();
            
            _entity.Collide();
            _entity.Update();
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