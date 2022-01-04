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
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
    public bool isDead = false;


    public void Attack()
    { 
       animator.SetTrigger("Punch");
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
       foreach (var enemy in hitEnemies)
       {
           enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
           Debug.Log("HIT " + enemy.name);
       }
        
    }
    public void PickUpFunc()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        grabCheck.collider.gameObject.transform.parent = boxHolder;
        grabCheck.collider.gameObject.transform.position = boxHolder.position;
        attackDamage += 10;
        Debug.Log("Picked Up! +10 Damage!");
        /* Drop */
        //grabCheck.collider.gameObject.transform.parent = null;

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
        Debug.Log("Player Died!");
        animator.SetBool("IsDead", true);
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

}
