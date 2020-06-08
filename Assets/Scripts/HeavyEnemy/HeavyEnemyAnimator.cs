using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAnimator : MonoBehaviour
{
    public Animator animator;

    public void TakeDamage()
    {

        animator.SetTrigger("TakeDamage");
    }

  

 

    public void Die()
    {
        animator.SetBool("isDead", true);
    }

    internal void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Run(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
}
