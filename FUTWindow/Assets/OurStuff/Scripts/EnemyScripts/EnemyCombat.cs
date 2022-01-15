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
    public SpriteRenderer sr;
    public PostBar postBar;
    public GameObject player;
    public GameObject floatingPoints;
    public GameObject enemyDrops;
    public int enemyEXP;
    public float attackRange = 5f;
    public int attackDamage;
    public float postBreakpoint = 25;
    public int maxPost = 100;
    public bool isDown = false;
    int currentPost;
    Vector3 knockbackTo;
    public int maxHP = 100;
    int currentHP;
    bool isDead = false;
    public bool isKnockBack;
    
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
        KnockBack();
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
                    SoundManagerScript.PlaySound("Punch");
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
                    SoundManagerScript.PlaySound("Upper");
                    break;
                }
            case "Hook":
                {
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    currentPost -= 5;
                    animator.SetTrigger("Hurt");
                    SoundManagerScript.PlaySound("Punch");
                    break;
                }
            case "PushKick":
                {
                    Damage = Damage + (100 / (currentPost + 1));
                    FloatingDamage(Damage);
                    currentHP -= Damage;

                    if (sr.flipX)
                    {
                        knockbackTo = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        knockbackTo = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                    }

                    if (currentPost <= maxPost * 0.25 && currentHP > 0)
                    {
                        isKnockBack = true;
                        isDown = true;
                        animator.SetTrigger("Down");
                    }
                    else if (currentPost <= maxPost * 0.25 && currentHP <= 0)
                    {
                        isKnockBack = true;
                        isDown = true;
                        animator.SetTrigger("Down");
                        Die();
                    }
                    else if (currentPost > maxPost * 0.25)
                    {
                        animator.SetTrigger("Hurt");
                    }
                    SoundManagerScript.PlaySound("LegKick");
                    break;
                }
            case "LowKick":
                {
                    FloatingDamage(Damage);
                    currentHP -= Damage;
                    currentPost -= 25;
                    enemyAI.speed = enemyAI.speed * 0.95f;
                    animator.SetTrigger("HitKick");
                    SoundManagerScript.PlaySound("LegKick");
                    break;
                }
            default:
                break;
        }
        
        if (currentHP <= 0)
        {
            ScoreScript.scoreValue += enemyEXP;
            Die();
            //enemy flashing before death
            Invoke("GetNoAlpha", 3.2f);
            Invoke("GetAlphaBack", 3.3f);
            Invoke("GetNoAlpha", 3.4f);
            Invoke("GetAlphaBack", 3.5f);
            Invoke("GetNoAlpha", 3.6f);
            Invoke("GetAlphaBack", 3.7f);
            Invoke("GetNoAlpha", 3.8f);
            Invoke("GetAlphaBack", 3.9f);
            //destroy enemy for wavespawn system
            int randomDrop = UnityEngine.Random.Range(1, 7);
            if (randomDrop == 4)
            {
                Instantiate(enemyDrops, this.transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 4);
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
        SoundManagerScript.PlaySound("Dead3");

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
            if (currentPost < maxPost && !isDown)
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

    void KnockBack()
    {
        if (isKnockBack)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, knockbackTo, Time.deltaTime * 4);
        }
    }

    void GetNoAlpha()
    {
        //enemy flashing before death
        sr.color = new Color(1f, 1f, 1f, 0f);
    }
    void GetAlphaBack()
    {
        //enemy flashing before death
        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    
}
