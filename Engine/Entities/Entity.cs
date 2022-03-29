using Engine.Entities.Components;

namespace Engine.Entities;

public abstract class Entity : IDisposable
{
    public readonly int Id;
    public bool AutoManageComponents = true;

    public event EventHandler? OnSpawn;
    public event EventHandler? OnDestroy;
    
    protected readonly List<Component> Components;

    protected abstract void Construct();
    protected abstract void Init();
    
    protected Entity()
    {
        Components = new List<Component>();
        
        Id = EntityManager.IncrementEntityId();
        EntityManager.Entities.Add(this);

        Construct();
        Init();
        OnSpawn?.Invoke(this, EventArgs.Empty);
    }
    
    public T? TryGetComponent<T>() where T : class
    {
        return Components.Find(_component => _component.GetType() == typeof(T)) as T;
    }
    
    public T GetComponent<T>() where T : class
    {
        var _component = TryGetComponent<T>();
        if (_component == null)
            throw new NullReferenceException($"Component {typeof(T)} was null");

        return _component;
    }

    public void AddComponent(params Component[] _components)
    {
        foreach (var _component in _components)
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
}