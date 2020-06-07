using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public PlayerAnimationController controller;
    public Transform attackPoint;

    public LayerMask enemyLayers;

    public int attackDamage = 20;
    public float attackRange = .5f;
    public float runAttackRange = 5f;
    public float idleAttackRange = 3.75f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;


    public PlayerMovement playerMovement;
    private Collider2D enemyColllider;
    public int currentHealth;
    public int maxHealth;

    private void Start() {

        currentHealth = maxHealth;

    }
    
       


    public void Attack()
    {
        if(Time.time >= nextAttackTime)
        {
            
            controller.Attack(); //animation

            

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemyColllider = enemy;
                Debug.Log("enemy -40" + enemy.name);
                StartCoroutine("DelayedAttack", 0.4f);
                

            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

   private IEnumerator DelayedAttack()
    {
        
        yield return new WaitForSeconds(0.4f); //Count is the amount of time in seconds that you want to wait.
        enemyColllider.GetComponent<HeavyEnemy>().TakeDamage();
        yield return null;
    }

    public void TakeDamage()
    {
        currentHealth -= 40;
        controller.TakeDamage();

        if (currentHealth <= 0)
        {

            Die();
        }
    }

    private void Die()
    {

        controller.Die();
    }

    private void Update()
    {
        if (Mathf.Abs(playerMovement.horizontal) > 0.01)
        {
            attackRange = runAttackRange;
        }
        else
        {
            attackRange = idleAttackRange;
        }
    }
}
