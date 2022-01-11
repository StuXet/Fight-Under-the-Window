using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public LayerMask playerLayers;
    public Transform attackPoint;
    public Animator animator;
    public HpBar healthBar;
    public EnemyAI enemyAI;
    public PostBar postBar;
    public GameObject player;
    public GameObject floatingPoints;
    public float attackRange = 5f;
    public int attackDamage;
    public float postBreakpoint = 25;
    public int maxPost = 100;
    public bool isDown = false;
    int currentPost;
    public int maxHP = 100;
    int currentHP;
    bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        currentPost = maxPost;  
        healthBar.SetHealth(currentHP, maxHP);
        playerLayers = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Cooldown");
        StartCoroutine("PostureRegenerator");
    }

    private void Update()
    {
        healthBar.SetHealth(currentHP, maxHP);
        postBar.SetPost(currentPost, maxPost, postBreakpoint);
        ZeroPost();
        IsDown();
    }

    public void Jab()
    {
        if (animator.GetBool("IsInRange") && !player.GetComponent<PlayerCombat>().isDead && !isDead && !isDown)
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
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    currentPost -= 20;
                    animator.SetTrigger("Hurt");
                    break;
                }
            case "Uppercut":
                {
                    Damage = Damage + (50 / (currentPost + 1));
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    if (currentPost <= maxPost * 0.25 && currentHP > 0)
                    {
                        isDown = true;  
                        animator.SetTrigger("Down");
                    }
                    else if (currentPost <= maxPost * 0.25 && currentHP <= 0)
                    {
                        isDown = true;
                        animator.SetTrigger("Down");
                        Die();
                    }
                    else if (currentPost > maxPost * 0.25)
                    {
                     animator.SetTrigger("Hurt");
                    }
                    break;
                }
            case "Hook":
                {
                    Damage = Damage * 2;
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    currentPost -= 5;
                    animator.SetTrigger("Hurt");
                    break;
                }
            case "PushKick":
                {
                    Damage = Damage + (100 / (currentPost + 1));
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    if (currentPost <= maxPost * 0.25 && currentHP > 0)
                    {
                        isDown = true;
                        animator.SetTrigger("Down");
                    }
                    else if (currentPost <= maxPost * 0.25 && currentHP <= 0)
                    {
                        isDown = true;
                        animator.SetTrigger("Down");
                        Die();
                    }
                    else if (currentPost > maxPost * 0.25)
                    {
                        animator.SetTrigger("Hurt");
                    }
                    break;
                }
            case "LowKick":
                {
                    Damage = Damage * 2;
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    currentPost -= 5;
                    animator.SetTrigger("Hurt");
                    break;
                }
            default:
                break;
        }
        
        if (currentHP <= 0)
        {
            Die();
            // added destroy enemy for wavespawn system
            Destroy(gameObject, 3);
        }
    }

    public void IsDown()
    {
        if (isDown)
        {
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EnemyAI>().enabled = false;

        }
        else
        {
            GetComponentInChildren<BoxCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<EnemyAI>().enabled = true; 

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

    void FloatingDamage(int dmg)
    {
        GameObject go = Instantiate(floatingPoints, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        go.GetComponentInChildren<TextMesh>().text = dmg.ToString();
        Destroy(go, 3);
    }

    void ZeroPost()
    {
        if (currentPost < 0)
        {
            currentPost = 0;
        }
    }
}
