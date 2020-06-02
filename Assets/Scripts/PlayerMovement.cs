using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{


    public CharacterController2D characterController;

    public Animator animator;

    public float runSpeed = 200f;
    bool jump = false;
    float horizontal = 0f;
    

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        characterController.Move(horizontal * runSpeed * Time.fixedDeltaTime, false, jump);
        jump = false;
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
        animator.SetBool("isJumping", true);
        jump = true;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

}
