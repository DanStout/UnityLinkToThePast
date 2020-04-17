using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float totalLifetime;

    private float lifetimeLeft;
    private Rigidbody2D body;

    void Awake()
    {
        lifetimeLeft = totalLifetime;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifetimeLeft -= Time.deltaTime;
        if (lifetimeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void Launch(Vector2 target)
    {
        body.velocity = target * speed;
    }
}
