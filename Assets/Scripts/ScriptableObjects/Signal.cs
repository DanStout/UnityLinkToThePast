using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        for(var i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void AddListener(SignalListener list)
    {
        listeners.Add(list);
    }

    public void RemoveListener(SignalListener list)
    {
        listeners.Remove(list);
    }
}
