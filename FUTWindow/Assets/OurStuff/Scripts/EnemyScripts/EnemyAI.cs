using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float chaseDistance;
    public float stopDistance;
    public float speed;
    public float yRange = 1;

    private float targetDistance;
    private float hor;
    private float yDistance;

    public Transform LposAttackPoint;
    public Transform RposAttackPoint;
    public Transform attackPoint;
    public GameObject target;
    public GameObject enemy;


    [SerializeField]
    Animator animator;

    [SerializeField]
    SpriteRenderer sR;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        yDistance = Mathf.Abs(transform.position.y - target.transform.position.y);
        if ((targetDistance < chaseDistance && targetDistance > stopDistance) || yDistance > yRange)
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
            sR.flipX = false;
            enemy.GetComponent<SpriteRenderer>().flipX = false;
            attackPoint.transform.position = LposAttackPoint.transform.position;
        }
        else
        {
            sR.flipX = true;
            enemy.GetComponent<SpriteRenderer>().flipX = true;
            attackPoint.transform.position = RposAttackPoint.transform.position;
        }
    }
}