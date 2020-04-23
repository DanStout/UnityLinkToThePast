using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Log : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public float patrolRadius;
    public bool fireBullets;
    public bool chasePlayer = true;
    public float bulletDelay;
    private float bulletDelayLeft;

    public GameObject projectile;

    public Transform[] path;
    private int curPathIdx;

    public Collider2D bounds;

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

    void Update()
    {
        if (bulletDelayLeft > 0)
        {
            bulletDelayLeft -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        var dist = Vector3.Distance(target.position, transform.position);

        // If player is in range and we either are unbounded OR they're in our bounds:
        if (dist <= chaseRadius && dist > attackRadius && (bounds == null || bounds.bounds.Contains(target.transform.position)))
        {
            if (state == EnemyState.Stagger || state == EnemyState.Attack)
            {
                return;
            }

            if (chasePlayer)
            {
                MoveTowards(target.position);
            }
            else if (fireBullets && bulletDelayLeft <= 0)
            {
                bulletDelayLeft = bulletDelay;
                var temp = target.transform.position - transform.position;
                var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
                bullet.GetComponent<Projectile>().Launch(temp);
                state = EnemyState.Walk;
                anim.SetBool("wakeUp", true);
            }
        }
        // Otherwise (player too far away, or are outside our bounds), either return to patrol OR go to sleep
        else if (dist > chaseRadius || (bounds != null && !bounds.bounds.Contains(target.transform.position)))
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
