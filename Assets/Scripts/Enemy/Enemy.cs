using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState state;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;

    protected Rigidbody2D body;

    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        health = maxHealth.initialValue;
    }

    public void Knock(float knockTime, float damage)
    {
        StartCoroutine(KnockCo(knockTime));
        health -= damage;
        if (health <= 0)
        {
            Die();
            gameObject.SetActive(false);
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            var obj = GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(obj, 1f);
        }
    }

    IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        body.velocity = Vector2.zero;
        state = EnemyState.Idle;
    }
}
