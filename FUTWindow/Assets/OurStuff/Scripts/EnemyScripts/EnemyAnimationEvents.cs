using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public Animator animator;

    void SetPunchFalse()
    {
        animator.SetBool("isPunching", false);
    }
}
