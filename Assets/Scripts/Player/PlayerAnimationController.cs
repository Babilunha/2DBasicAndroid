using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    public void setSpeed(float horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    public void Jump(bool jump)
    {
        animator.SetBool("isJumping", jump);
        
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

   
}
