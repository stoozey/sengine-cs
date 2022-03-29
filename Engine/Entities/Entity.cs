using Engine.Entities.Components;

namespace Engine.Entities;

public abstract class Entity : IDisposable
{
    public readonly int Id;
    public event EventHandler? OnDestroy;
    public bool AutoManageComponents = true;

    protected readonly List<Component> Components;
    
    public T? TryGetComponent<T>() where T : class
    {
        return Components.Find(_component => _component.GetType() == typeof(T)) as T;
    }
    
    public T GetComponent<T>() where T : class
    {
        var _component = TryGetComponent<T>();
        if (_component == null)
            throw new NullReferenceException($"Component {typeof(T)} was null");
        
        return (T) TryGetComponent<T>();
    }

    public void AddComponent(Component _component)
    {
        Components.Add(_component);
    }

    public bool RemoveComponent(Component _component)
    {
        return Components.Remove(_component);
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