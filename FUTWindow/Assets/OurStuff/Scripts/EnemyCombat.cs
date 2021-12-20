using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public int maxHP = 100;
    int currentHP;
    public float attackRange = .5f;
    public LayerMask playerLayers;
    public Transform attackPoint;
    public Animator animator;

 
    void Start()
    {
        currentHP = maxHP;
    }

    public void Attack()
    {
        animator.SetBool("Punch", true);
        animator.Play("EnemyPunch4");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var player in hitPlayer)
        {
            Debug.Log("hit " + player.name);
        }
    }

    public void TakeDamage(int Damage)
    {
        currentHP -= Damage;

        if (currentHP == 0)
        {
            Die();
        }
    }

    void Die()
    {
         Debug.Log("Enemy Died!");
    }
    
}
