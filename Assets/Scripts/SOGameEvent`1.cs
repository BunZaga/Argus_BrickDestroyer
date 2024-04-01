using System;
using System.Collections.Generic;
using UnityEngine;

public class SOGameEvent<T>:ScriptableObject
{
    private List<Action<T>> listeners = new List<Action<T>>();
    private Action<T> action = null;
    
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