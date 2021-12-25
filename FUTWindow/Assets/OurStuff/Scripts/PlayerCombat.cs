using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;
    public Animator animator;
    public Transform attackPoint;
    public int attackDamage = 15;
    public float attackRange = .5f;
    public LayerMask enemyLayers;


    public void Attack()
    {
        animator.SetTrigger("Punch");
        animator.Play("Punch1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
            Debug.Log("HIT " + enemy.name);
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
    void Update()
    {
        
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
        Debug.Log("Player Died!");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
