using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAnimator : MonoBehaviour
{
    public Animator animator;

    public void TakeDamage()
    {

        Invoke("TakeDamageDelayed", .4f);
    }

    private void TakeDamageDelayed()
    {
        animator.SetTrigger("TakeDamage");
    }

 

    public void Die()
    {
        animator.SetBool("isDead", true);
    }
}
