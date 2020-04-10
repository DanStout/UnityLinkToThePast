using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool inRange;

    public Signal contextChanged;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (ShowContextChanged())
            {
                contextChanged.Raise();
            }
            inRange = true;
            OnPlayerEnter();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (ShowContextChanged())
            {
                contextChanged.Raise();
            }
            inRange = false;
            OnPlayerLeave();
        }
    }

    protected virtual bool ShowContextChanged()
    {
        return true;
    }

    protected virtual void OnPlayerEnter()
    {

    }

    protected virtual void OnPlayerLeave()
    {

    }
}
