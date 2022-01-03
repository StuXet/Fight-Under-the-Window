using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public GameObject target;
    public GameObject enemy;
    private float hor;
    public Transform RposAttackPoint;
    public Transform LposAttackPoint;
    public Transform attackPoint;

    private float targetDistance;

    [SerializeField]
    Animator animator;

    [SerializeField]
    SpriteRenderer sR;

    void Start()
    {
    }


    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        if (targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            ChasePlayer();
            animator.SetBool("IsInRange", false);

        }
        else
        {
            StopChasePlayer();
            animator.SetBool("IsInRange", true);
        }

        Flip();
    }

    private void StopChasePlayer()
    {
        animator.SetBool("IsWalking", false);
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        animator.SetBool("IsWalking", true);
    }

    private void Flip()
    {
        if (transform.position.x < target.transform.position.x)
        {
            sR.flipX = true;
            enemy.GetComponent<SpriteRenderer>().flipX = true;
            attackPoint.transform.position = LposAttackPoint.transform.position;
        }
        else
        {
            sR.flipX = false;
            enemy.GetComponent<SpriteRenderer>().flipX = false;
            attackPoint.transform.position = RposAttackPoint.transform.position;
        }
    }
}