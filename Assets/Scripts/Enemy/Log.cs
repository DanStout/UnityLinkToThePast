using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Log : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public float patrolRadius;
    public Transform homePosition;

    public Transform[] path;
    private int curPathIdx;

    private Transform target;
    private Animator anim;
    private bool onPatrol;

    new void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        onPatrol = path.Length > 0;
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

            MoveTowards(target.position);
        }
        else if (dist > chaseRadius)
        {
            if (onPatrol)
            {
                var pathPt = path[curPathIdx].position;
                if (Vector3.Distance(pathPt, transform.position) > patrolRadius)
                {
                    MoveTowards(pathPt);
                }
                else
                {
                    curPathIdx++;
                    if (curPathIdx >= path.Length)
                    {
                        curPathIdx = 0;
                    }
                }
            }
            else
            {
                anim.SetBool("wakeUp", false);
            }
        }
    }

    void MoveTowards(Vector3 point)
    {
        var temp = Vector3.MoveTowards(transform.position, point, moveSpeed * Time.deltaTime);
        var dir = temp - transform.position;
        anim.SetFloat("moveX", dir.x);
        anim.SetFloat("moveY", dir.y);
        body.MovePosition(temp);
        state = EnemyState.Walk;
        anim.SetBool("wakeUp", true);
    }
}
