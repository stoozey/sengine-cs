using Engine.Entities.Components;

namespace Engine.Entities;

public abstract class Entity : IDisposable
{
    public readonly int Id;
    public event EventHandler? OnDestroy;
    public bool AutoManageComponents = true;

    protected readonly List<Component> Components;
    
    public T? GetComponent<T>() where T : class
    {
        return Components.Find(_component => _component.GetType() == typeof(T)) as T;
    }
    
    public void RenderComponents()
    {
        foreach (var _component in Components)
        {
            _component.Render();
        }
    }
    
    public void UpdateComponents()
    {
        foreach (var _component in Components)
        {
            _component.Update();
        }
    }
    
    public void Destroy()
    {
        OnDestroy?.Invoke(this, EventArgs.Empty);
        
        EntityManager.Entities.Remove(this);
        Dispose();
    }

    public abstract void Render();
    public abstract void Collide();
    public abstract void Update();

    public virtual void Dispose() { }

    protected Entity()
    {
        Components = new List<Component>();
        
        Id = EntityManager.IncrementEntityId();
        EntityManager.Entities.Add(this);
    }
}