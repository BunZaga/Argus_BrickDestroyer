using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOGameEvent : ScriptableObject
{
    private List<Action> listeners = new List<Action>();
    private Action action = null;
    
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