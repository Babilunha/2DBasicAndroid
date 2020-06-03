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
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Button attackButton;

    

    // Update is called once per frame
    //void Update()
    //{
    //    if(Time.time >= nextAttackTime)
    //    {
    //        if(attackButton.OnPointerDown(Attack()))
    //        {
    //            Attack();
    //            nextAttackTime = Time.time + 1f / attackRate;
    //        }
            
    //    }
    //}

    public void Attack()
    {
        if(Time.time >= nextAttackTime)
        {
            
            controller.Attack(); //animation

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HeavyEnemy>().TakeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
