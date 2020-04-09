using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable") && gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }

        var isEnemy = other.CompareTag("Enemy");
        var isPlayer = other.CompareTag("Player");


        if (isEnemy || isPlayer)
        {
            var hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                var dir = hit.transform.position - transform.position;
                var force = dir.normalized * thrust;
                hit.AddForce(force, ForceMode2D.Impulse);

                if (isEnemy && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().state = EnemyState.Stagger;
                    other.GetComponent<Enemy>().Knock(knockTime, damage);
                }
                else if (isPlayer)
                {
                    hit.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                }
            }
        }
    }
}
