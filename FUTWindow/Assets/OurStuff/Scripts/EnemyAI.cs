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
        }
        else
        {
            sR.flipX = false;
        }
    }



    //public Transform attackPoint;
    //public float attackRange = .5f;
    //public LayerMask playerLayers;

    //public void Attack()
    //{
    //    animator.SetBool("Punch", true);
    //    animator.Play("EnemyPunch4");
    //    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
    //    foreach (var player in hitPlayer)
    //    {
    //        Debug.Log("hit " + player.name);
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}
}