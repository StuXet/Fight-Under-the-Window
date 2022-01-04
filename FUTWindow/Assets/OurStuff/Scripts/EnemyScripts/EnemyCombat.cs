using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public LayerMask playerLayers;
    public Transform attackPoint;
    public Animator animator;
    public HpBar healthBar;
    public PostBar postBar;
    public GameObject player;
    public float attackRange = 5f;
    public int attackDamage;
    public float postBreakpoint = 25;
    public int maxPost = 100;
    int currentPost;
    public int maxHP = 100;
    int currentHP;
    bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        currentPost = maxPost;  
        healthBar.SetHealth(currentHP, maxHP);
        StartCoroutine("Cooldown");
        StartCoroutine("PostureRegenerator");
    }

    private void Update()
    {
        healthBar.SetHealth(currentHP, maxHP);
        postBar.SetPost(currentPost, maxPost, postBreakpoint);
        ZeroPost();
    }

    public void Jab()
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


    public void TakeDamage(int Damage, string attackType)
    {

        switch (attackType)
        {
            case "Jab":
                {
                    currentHP -= Damage;
                    currentPost -= 20;
                    animator.SetTrigger("Hurt");
                    break;
                }
            case "Uppercut":
                {
                    currentHP -= Damage + (150 / (currentPost + 1));
                    animator.SetTrigger("Hurt");
                    break;
                }
            default:
                break;
        }
        
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
            Jab();
        }
    }
    
    IEnumerator PostureRegenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (currentPost < maxPost)
            {
                currentPost += 5;
            }
        }
    }

    void ZeroPost()
    {
        if (currentPost < 0)
        {
            currentPost = 0;
        }
    }
}
