using System;
using System.Collections.Generic;

[System.Serializable]
public class GameEvent
{
    private List<Action> listeners = new List<Action>();
    private Action action = null;

    public GameEvent() { }

    public GameEvent(Action action)
    {
        this.action = action;
    }

    public void SetEventHandler(Action action)
    {
        this.action = action;
    }

    public void Invoke()
    {
        if (action != null) { action(); }

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
            {
                listeners[i].Invoke();
            }
        }
    }

    public void AddListener(Action action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
        listeners.Add(action);
    }

    public void RemoveListener(Action action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
    }
}

public class GameEvent<T>
{
    private List<Action<T>> listeners = new List<Action<T>>();
    private Action<T> action = null;

    public GameEvent() { }

    public GameEvent(Action<T> action)
    {
        this.action = action;
    }

    public void SetEventHandler(Action<T> action)
    {
        this.action = action;
    }

    public void Invoke(T t)
    {
        if (action != null) { action(t); }

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
            {
                listeners[i].Invoke(t);
            }
        }
    }

    public void AddListener(Action<T> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
        listeners.Add(action);
    }

    public void RemoveListener(Action<T> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
    }
}

public class GameEvent<T1, T2>
{
    private List<Action<T1, T2>> listeners = new List<Action<T1, T2>>();
    private Action<T1, T2> action = null;

    public GameEvent() { }

    public GameEvent(Action<T1, T2> action)
    {
        this.action = action;
    }

    public void SetEventHandler(Action<T1, T2> action)
    {
        this.action = action;
    }

    public void Invoke(T1 t1, T2 t2)
    {
        if (action != null) { action(t1, t2); }

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
            {
                listeners[i].Invoke(t1, t2);
            }
        }
    }

    public void AddListener(Action<T1, T2> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
        listeners.Add(action);
    }

    public void RemoveListener(Action<T1, T2> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
    }
}

public class GameEvent<T1, T2, T3>
{
    private List<Action<T1, T2, T3>> listeners = new List<Action<T1, T2, T3>>();
    private Action<T1, T2, T3> action = null;

    public GameEvent() { }

    public GameEvent(Action<T1, T2, T3> action)
    {
        this.action = action;
    }

    public void SetEventHandler(Action<T1, T2, T3> action)
    {
        this.action = action;
    }

    public void Invoke(T1 t1, T2 t2, T3 t3)
    {
        if (action != null) { action(t1, t2, t3); }

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
            {
                listeners[i].Invoke(t1, t2, t3);
            }
        }
    }

    public void AddListener(Action<T1, T2, T3> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
        listeners.Add(action);
    }

    public void RemoveListener(Action<T1, T2, T3> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
    }
}

public class GameEvent<T1, T2, T3, T4>
{
    private List<Action<T1, T2, T3, T4>> listeners = new List<Action<T1, T2, T3, T4>>();
    private Action<T1, T2, T3, T4> action = null;

    public GameEvent() { }

    public GameEvent(Action<T1, T2, T3, T4> action)
    {
        this.action = action;
    }

    public void SetEventHandler(Action<T1, T2, T3, T4> action)
    {
        this.action = action;
    }

    public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4)
    {
        if (action != null) { action(t1, t2, t3, t4); }

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
            {
                listeners[i].Invoke(t1, t2, t3, t4);
            }
        }
    }

    public void AddListener(Action<T1, T2, T3, T4> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
        listeners.Add(action);
    }

    public void RemoveListener(Action<T1, T2, T3, T4> action)
    {
        if (listeners.Contains(action))
        {
            listeners.Remove(action);
        }
    }
}