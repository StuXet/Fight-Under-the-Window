using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    
    public int maxHP = 100;
    int currentHP; 
    public Animator animator;
    public Transform attackPoint;
    public int attackDamage = 15;
    public float attackRange = .5f;
    public LayerMask playerLayers;

 

    public void Attack()
    {
        animator.SetBool("Punch", true);
        animator.Play("EnemyPunch4");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            Debug.Log("hit " + player.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
       if (attackPoint == null)
           return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int Damage)
    {
        currentHP -= Damage;
        animator.SetTrigger("Hurt");
        if (currentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
         Debug.Log("Enemy Died!");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        this.enabled = false;
    }
}
