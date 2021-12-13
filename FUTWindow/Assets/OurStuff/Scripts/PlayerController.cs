using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public Rigidbody2D rb;
    public FixedJoystick joystick;
    public float movementSpeed = 10;
    private float hor;
    private float ver;
    private string dir;

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    public void CheckInput()
    {
        hor = joystick.Horizontal;
        ver = joystick.Vertical;
    }

    public void Move()
    {
        if (Mathf.Abs(hor) >= 0.5f || Mathf.Abs(ver) >= 0.5f)
        {
            rb.velocity = new Vector2(hor * movementSpeed, ver * movementSpeed);
        }
        else if (Mathf.Abs(hor) == 0 && Mathf.Abs(ver) == 0)
        {
            rb.velocity = new Vector2(hor * movementSpeed, ver * movementSpeed);
        }
        else if (Mathf.Abs(hor) < 0.5f || Mathf.Abs(ver) < 0.5f)
        {
            rb.velocity = new Vector2( 0, ver * 0 );
        }
    }

    public void Animate()
    {
        FlipX();
        Walk();
    }

    public void punch()
    {
        anim.Play("Punch2");
        anim.Play("Punch1");
    }
    public void Defend()
    {
        anim.Play("Defend");
    }
    public void punch2()
    {
        anim.Play("Kick2");
        anim.Play("Kick1");
    }

    //Animations
    private void Walk()
    {
        if (Mathf.Abs(hor) >= 0.5f || Mathf.Abs(ver) >= 0.5f)
        {
            anim.SetBool("IsWalking", true);
        }
        else if (Mathf.Abs(hor) < 0.5f || Mathf.Abs(ver) < 0.5f)
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private void FlipX()
    {
        if (hor < 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (hor > 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
   
}
