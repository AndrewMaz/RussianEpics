using System;

public abstract class Event
{
    public event Action OnStart;
    public event Action OnFinish;

    public virtual void Start()
    {
        OnStart?.Invoke();
    }
    public virtual void Finish()
    {
        OnFinish?.Invoke();
    }
}
