using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    private Transform target;
    private Animator anim;

    new void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        var dist = Vector3.Distance(target.position, transform.position);

        if (dist <= chaseRadius && dist > attackRadius)
        {
            if (state == EnemyState.Stagger || state == EnemyState.Attack)
            {
                return;
            }

            var temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            var dir = temp - transform.position;
            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
            body.MovePosition(temp);

            state = EnemyState.Walk;
            anim.SetBool("wakeUp", true);
        }
        else if(dist > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

}
