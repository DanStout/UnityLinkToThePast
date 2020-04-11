using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool inRange;

    readonly List<ContextClue> activeContextClues = new List<ContextClue>();

    protected virtual bool ShowContextClue()
    {
        return true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ShowContextClue())
        {
            var ctx = other.GetComponent<ContextClue>();
            if (ctx != null)
            {
                activeContextClues.Add(ctx);
                ctx.ShowClue();
            }
        }

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            inRange = true;
            OnPlayerEnter();
        }
    }

    protected void UpdateActiveContextClues(bool shown)
    {
        foreach (var clue in activeContextClues)
        {
            clue.ShowClue(shown);
        }

        if (!shown)
        {
            activeContextClues.Clear();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var ctx = other.GetComponent<ContextClue>();
        if (ctx != null)
        {
            activeContextClues.Remove(ctx);
            ctx.ShowClue(false);
        }

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            inRange = false;
            OnPlayerLeave();
        }
    }

    protected virtual void OnPlayerEnter()
    {

    }

    protected virtual void OnPlayerLeave()
    {

    }
}
