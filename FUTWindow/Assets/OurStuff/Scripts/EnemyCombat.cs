using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    
    public int maxHP = 100;
    int currentHP; 
    public Animator animator;
    public Transform attackPoint;
    public int attackDamage;
    public float attackRange = .5f;
    public LayerMask playerLayers;
    public GameObject player;
 

    public void Attack()
    {
        if (animator.GetBool("IsInRange"))
        {
            animator.SetBool("Punch", true);
            animator.Play("EnemyPunch4");
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
            
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
        StartCoroutine("Cooldown");
    }
    private void Update()
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
         Debug.Log("Enemy Died!");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        this.enabled = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        Attack();
    }
}
