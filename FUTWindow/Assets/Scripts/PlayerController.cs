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
        //Animate();
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
    }

    public void Animate()
    {
        
    }
   
}
