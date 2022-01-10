using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public LayerMask playerLayers;
    public Transform attackPoint;
    public Animator animator;
    public GameObject player;
    public float attackRange = 5f;
    public int attackDamage;
    public int maxHP = 100;
    int currentHP;
    IEnumerator coroutine;

    void Start()
    {
        currentHP = maxHP;
        playerLayers = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        coroutine = Cooldown();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
    }

    public void Attack()
    {
        if (animator.GetBool("IsInRange") && !player.GetComponent<PlayerCombat>().isDead)
        {
            animator.SetBool("isPunching", true);
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
        Destroy(gameObject, 3);
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
