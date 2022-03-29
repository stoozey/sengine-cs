namespace Engine.Entities.Components;

public abstract class Component
{
    protected Entity Owner;

    public event EventHandler? OnDestroy;
    
    public abstract void Render();
    public abstract void Update();

    protected T Require<T>() where T : class
    {
        T? _component = Owner.GetComponent<T>();
        if (_component == null)
            throw new MissingComponentException($"{GetType()} component could not find {typeof(T)} from owner entity");

        return _component;
    }
    
    protected Component(Entity _owner)
    {
        Owner = _owner;

        Owner.OnDestroy += (_sender, _args) => OnDestroy?.Invoke(this, EventArgs.Empty);
    }
}

public class MissingComponentException : Exception
{
    public MissingComponentException(string _message) : base(_message) { }
}