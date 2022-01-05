using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public Animator animator;
    public EnemyCombat combat;

    void SetPunchFalse()
    {
        animator.SetBool("isPunching", false);
    }
    void SetDownFalse()
    {
        combat.isDown = false;
    }
}
