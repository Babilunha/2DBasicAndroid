using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //moving
    public float runSpeed;
    private float currentSpeed;
    private bool movingLeft = true;

    //distance to check for the ground
    public float groundCheckDistance;
    //distance to check for the player
    public float playerCheckDistance;

    //check if there's a ground in front of AI
    public Transform groundInFrontDetection;
    //check if there's a player in front of AI
    public Transform playerInFrontDetection;

    //Animator for the AI
    public HeavyEnemyAnimator animator;

    //Health & Damage
    public float currentHealth;

    //dead body image
    public GameObject deadbody;

    //death handling
    public BoxCollider2D aliveCollider;
    public BoxCollider2D deathCollider;
    private int deathInt = 1;

    


    private void Update()
    {
        if(deathInt == 1)
        {
            PlayerCheck();
            Run(currentSpeed);
            Attack();
        }
        

        
    }

    private void Attack()
    {
        
    }

    private void PlayerCheck()
    {
        RaycastHit2D playerInfo;

        if (!movingLeft)
        {
            playerInfo = Physics2D.Raycast(playerInFrontDetection.position, Vector2.right, playerCheckDistance);
            Debug.DrawRay(playerInFrontDetection.position, Vector2.right * playerCheckDistance, Color.red );
        } else
        {
            playerInfo = Physics2D.Raycast(playerInFrontDetection.position, Vector2.left, playerCheckDistance);
            Debug.DrawRay(playerInFrontDetection.position, Vector2.left * playerCheckDistance, Color.red);

        }

        if (playerInfo.collider != null && playerInfo.collider.tag.Equals("Player"))
        {
            currentSpeed = 0f;
        } else
        {
            currentSpeed = runSpeed;
        }

    }

    void Run(float speed)
    {
        animator.Run(speed);
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundInFrontDetection.position, Vector2.down, groundCheckDistance);


        if (groundInfo.collider == null || !groundInfo.collider.tag.Equals("Ground"))
        {
            //Debug.Log("not touching ground");
            if (movingLeft)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                movingLeft = true;
            }
        }
    }

    public void TakeDamage()
    {
        if (deathInt == 1)
        {
            currentHealth -= 40;
            animator.TakeDamage();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        aliveCollider.enabled = false;
        deathCollider.enabled = true;

        animator.Die();
        deathInt = 0;
        
    }
}
