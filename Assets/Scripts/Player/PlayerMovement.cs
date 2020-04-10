using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Attack,
    Interact,
    Stagger,
    Idle
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public FloatValue currentHealth;
    public PlayerState state;
    public Signal healthSignal;
    public VectorValue positionStorage;
    public Inventory inventory;
    public SpriteRenderer heldItemSprite;

    private Rigidbody2D body;
    private Vector3 change;
    private Animator anim;

    void Start()
    {
        state = PlayerState.Walk;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);

        transform.position = positionStorage.initialValue;
    }

    void Update()
    {
        if (state == PlayerState.Interact)
        {
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && state != PlayerState.Attack && state != PlayerState.Stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (state == PlayerState.Walk || state == PlayerState.Idle)
        {
            var hasChange = change != Vector3.zero;
            anim.SetBool("moving", hasChange);

            if (hasChange)
            {
                Move();
                anim.SetFloat("moveX", change.x);
                anim.SetFloat("moveY", change.y);
            }
        }
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        state = PlayerState.Attack;
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        state = PlayerState.Walk;
    }

    public void RaiseItem()
    {
        if (state == PlayerState.Interact)
        {
            anim.SetBool("receiveItem", false);
            state = PlayerState.Idle;
            heldItemSprite.sprite = null;
        }
        else
        {
            anim.SetBool("receiveItem", true);
            state = PlayerState.Interact;
            heldItemSprite.sprite = inventory.currentItem.sprite;
        }
    }

    public void Knock(float knockTime, float damage)
    {
        if (state != PlayerState.Stagger)
        {
            currentHealth.runtimeValue -= damage;
            healthSignal.Raise();
            if (currentHealth.runtimeValue > 0)
            {
                StartCoroutine(KnockCo(knockTime));
                state = PlayerState.Stagger;
            }
            else
            {
                Application.Quit();
            }
        }
    }

    IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        body.velocity = Vector2.zero;
        state = PlayerState.Idle;
    }

    void Move()
    {
        body.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
    }
}
