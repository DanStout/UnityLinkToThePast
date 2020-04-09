using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    void OnEnable()
    {
        signal.AddListener(this);
    }

    void OnDisable()
    {
        signal.RemoveListener(this);
    }
}
