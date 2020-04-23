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
    public string enemyName;
    public GameObject deathEffect;
    public Signal signalDeactivated;

    [Header("Stats")]
    public FloatValue maxHealth;
    public float health;
    public int baseAttack;
    public float moveSpeed;
   
    protected Rigidbody2D body;
    private Vector2 startPosition;

    void Awake()
    {
        health = maxHealth.initialValue;
        startPosition = transform.position;
    }

    void OnEnable()
    {
        transform.position = startPosition;
    }

    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Knock(float knockTime, float damage)
    {
        StartCoroutine(KnockCo(knockTime));
        health -= damage;
        if (health <= 0)
        {
            Die();
            gameObject.SetActive(false);
            signalDeactivated.Raise();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            var obj = Instantiate(deathEffect, transform.position, Quaternion.identity);
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
