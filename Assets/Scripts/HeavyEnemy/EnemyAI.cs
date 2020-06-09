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

    //technical values
    private int deathInt = 1;
    private int runInt = 1;
    bool attacking = false;
    private float nextAttackTime = 0f;
    private float attackRate = 0.5f;

    public LayerMask enemyLayers;
    private float attackRange = 4f;
    public Transform attackPoint;
    private Collider2D enemyColllider;

    private void Update()
    {
        if (deathInt == 1 && attacking == false)
        {
            PlayerCheck();
            Run(currentSpeed);
            
        } 
        if (deathInt == 1 && attacking == true)
        {
            Attack();
        }
        




    }

    private void Attack()
    {
        

        if (Time.time >= nextAttackTime)
        {

            animator.Attack(); //animation
            



            Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

            if (hitEnemies != null)
            {
                enemyColllider = hitEnemies;
                //Debug.Log("enemy -40" + hitEnemies.name);
                StartCoroutine("DelayedAttack", 0.4f);
            }



            PlayerCheck();
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }

    private IEnumerator DelayedAttack()
    {

        yield return new WaitForSeconds(0.4f); //Count is the amount of time in seconds that you want to wait.
        enemyColllider.GetComponent<PlayerCombat>().TakeDamage();
        yield return null;
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
            attacking = true;
            currentSpeed = 0f;
        } else
        {
            attacking = false;
            if (runInt == 1)
            {
                currentSpeed = runSpeed;
                
            }
            
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
            runInt = 0;
            currentSpeed = 0;
            
            Invoke("ReturnSpeed", 0.3f);


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

    private void ReturnSpeed()
    {
        runInt = 1;
    }
}
