using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public LayerMask playerLayers;
    public Transform attackPoint;
    public Animator animator;
    public HpBar healthBar;
    public GameObject player;
    public float attackRange = 5f;
    public int attackDamage;
    public int maxHP = 100;
    bool isDead = false;
    int currentHP;
    IEnumerator coroutine;

    void Start()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP, maxHP);
        coroutine = Cooldown();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHP, maxHP);
    }

    public void Attack()
    {
        if (animator.GetBool("IsInRange") && !player.GetComponent<PlayerCombat>().isDead && !isDead)
        {
            animator.SetBool("isPunching", true);
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
            if (hitPlayer.Length > 0)
            {
                hitPlayer[0].GetComponent<PlayerCombat>().TakeDamage(attackDamage);
                Debug.Log("hit " + player.name);
            }
            //player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
        GetComponentInChildren<Canvas>().enabled = false;
        isDead = true;
        this.enabled = false;
    }

    IEnumerator Cooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Attack();
        }
    }
}
