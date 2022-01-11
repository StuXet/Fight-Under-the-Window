using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;
    public Animator animator;
    public Transform attackPoint;
    public int jabDamage = 3;
    public int uppercutDamage = 15;
    public float attackRange = .5f;
    public LayerMask enemyLayers;
    public PlayerHpBar healthBar;
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
    [Range(0f, 1f)]
    public float blockReducer;
    public bool isDead = false;
    bool isBlocking = false;
    public float attackTimer;
    bool runAttackTimer = false;

    void Start()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP, maxHP);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHP, maxHP);
        StartAttackTimer();
    }
    public void Jab()
    { 
        if (attackTimer < 0.3)
        {
            animator.SetTrigger("Jab");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyCombat>().TakeDamage(jabDamage, "Jab");
                Debug.Log("HIT " + enemy.name);
            }
        }
        else if (attackTimer >= 0.3)
        {
           animator.SetTrigger("Hook");
           Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
           foreach (var enemy in hitEnemies)
           {
               enemy.GetComponent<EnemyCombat>().TakeDamage(jabDamage, "Hook");
               Debug.Log("HIT " + enemy.name);
           }
        }
        attackTimer = 0;
    }

    public void Kick()
    {
        if (attackTimer < 0.3)
        {
            animator.SetTrigger("LowKick");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyCombat>().TakeDamage(jabDamage, "LowKick");
                Debug.Log("HIT " + enemy.name);
            }
        }
        else if (attackTimer >= 0.3)
        {
            animator.SetTrigger("PushKick");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyCombat>().TakeDamage(jabDamage, "PushKick");
                Debug.Log("HIT " + enemy.name);
            }
        }
        attackTimer = 0;
    }

    public void UpperCut()
    {
        animator.SetTrigger("Uppercut");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().TakeDamage(uppercutDamage, "Uppercut");
            Debug.Log("HIT " + enemy.name);
        }

    }


    public void PickUpFunc()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        grabCheck.collider.gameObject.transform.parent = boxHolder;
        grabCheck.collider.gameObject.transform.position = boxHolder.position;
        jabDamage += 10;
        Debug.Log("Picked Up! +10 Damage!");
        /* Drop */
        //grabCheck.collider.gameObject.transform.parent = null;

    }

    public void ToggleBlock()
    {
        if (isBlocking)
        {
            animator.SetBool("IsBlocking", false);
            isBlocking = false;
        }
        else
        {
            animator.SetBool("IsBlocking", true);
            isBlocking = true;
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
        if (isBlocking)
        {
            currentHP -= Mathf.RoundToInt(Damage - Damage * blockReducer);
            animator.SetTrigger("Hurt");
        }
        else
        {
            currentHP -= Damage;
            animator.SetTrigger("Hurt");
        }
        
        if (currentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        healthBar.SetHealth(0, maxHP);
        Debug.Log("Player Died!");
        animator.SetBool("IsDead", true);
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

    void StartAttackTimer()
    {
        if (runAttackTimer)
        {
            attackTimer += Time.deltaTime;
        }
    }

    public void SetRatTrue()
    {
        runAttackTimer = true;
    }
    public void SetRatFalse()
    {
        runAttackTimer = false;
    }

}
