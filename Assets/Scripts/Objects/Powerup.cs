using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public Signal powerupSignal;

    void Start()
    {
        if (!GetComponents<BoxCollider2D>().Any(x => x.isTrigger))
        {
            Debug.LogError("Powerup has no trigger box collider 2D");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            OnPickedUp();
            powerupSignal.Raise();
            Destroy(gameObject);
        }
    }

    protected virtual void OnPickedUp()
    {

    }
}
