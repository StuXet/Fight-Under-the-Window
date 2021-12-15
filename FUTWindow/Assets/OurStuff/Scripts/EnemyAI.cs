using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public GameObject target;

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
        }
        else
        {
            StopChasePlayer();
        }
    }

    private void StopChasePlayer()
    {
        animator.SetBool("IsWalking", false);
    }

    private void ChasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            sR.flipX = true;
        }
        else
        {
            sR.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        animator.SetBool("IsWalking", true);
    }
}