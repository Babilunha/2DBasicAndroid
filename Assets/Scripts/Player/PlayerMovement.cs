using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody;

    public new Joystick joystick;
    public  CircleCollider2D collider2D;

    public LayerMask ground;


    
    public PlayerAnimationController playerAnimationController;

    public float runSpeed = 200f;
    public float jumpForce = 100f;
    bool isJumping = false;
    public float horizontal = 0f;
    private bool isFacingRight = true;

    
    private void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {


        calculateDirectionAndSpeed();
    }
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
        float extraHeight = .5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider2D.bounds.center,new Vector2(2,3) , 0f,  Vector2.down, extraHeight, ground); //in the new vector first value is horizontal, second - vertical
        return raycastHit.collider != null ;
    }



    private void PlayAnimation()
    {
        playerAnimationController.setSpeed(horizontal);
        playerAnimationController.Jump(isJumping);

        if (IsGrounded())
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
            //Debug.Log("jumping is true");
        }
    }

    void calculateDirectionAndSpeed()
    {
        float horizontalIntegerValue = 0;
        if (joystick.Horizontal > 0)
        {
            horizontalIntegerValue = 1;
        }
        else if (joystick.Horizontal < 0)
        {
            horizontalIntegerValue = -1;
        }
        else if (joystick.Horizontal == 0)
        {
            horizontalIntegerValue = 0;
        }

        horizontal = horizontalIntegerValue;
    }
}
