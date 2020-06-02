using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody;

    
    public PlayerAnimationController playerAnimationController;

    public float runSpeed = 200f;
    public float jumpForce = 100f;
    bool jump = false;
    float horizontal = 0f;
    private bool isFacingRight = true;

    [SerializeField] private GroundCheck groundCheck;


    private void FixedUpdate()
    {
        PlayAnimation();

        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidbody.velocity = new Vector2(horizontal * runSpeed, rigidbody.velocity.y);

        

        // If the input is moving the player right and the player is facing left...
        if (horizontal > 0 && !isFacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontal < 0 && isFacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    public void Left()
    {
        horizontal = -1f;

    }
    public void Right()
    {
        horizontal = 1f;
    }
    public void Stop()
    {
        horizontal = 0f;  
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.velocity = Vector2.up * jumpForce;
            
        }
        
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool IsGrounded()
    {
        return groundCheck.isGrounded;
    }



    private void PlayAnimation()
    {
        playerAnimationController.setSpeed(horizontal);
        playerAnimationController.Jump(jump);

        if (IsGrounded())
        {
            jump = false;
        }
        else
        {
            jump = true;
        }
    }

}
