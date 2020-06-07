
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    public HeavyEnemyAnimator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public int damageTaken;



    public BoxCollider2D deathCollider;
    public BoxCollider2D aliveCollider;

  
    public float attackRange = 4f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint;
    public LayerMask playerLayer;
    private Collider2D playerCollider;

    public HeavyEnemy heavyEnemy;
    public EnemyMovement enemyMovement;



    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        
        currentHealth -= 40;
        animator.TakeDamage();

        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {



        //GetComponent<Rigidbody2D>().isKinematic = true;
        //GetComponent<Collider2D>().enabled = false;
        aliveCollider.enabled = false;
        deathCollider.enabled = true;
     
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        animator.Die();
        heavyEnemy.enabled = false;
        enemyMovement.enabled = false;




    }

    internal void Attack()
    {
        if (Time.time >= nextAttackTime)
        {

            Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

           if(player != null)
            {
                playerCollider = player;
                StartCoroutine("DelayedAttack", 0.4f);
                

                animator.Attack();
                
                Debug.Log("-40" + player.name);
            }

                
                

            
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(0.4f); //Count is the amount of time in seconds that you want to wait.
        playerCollider.GetComponent<PlayerCombat>().TakeDamage();
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
