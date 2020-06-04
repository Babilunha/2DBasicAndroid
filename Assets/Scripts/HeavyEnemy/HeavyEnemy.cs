using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    public HeavyEnemyAnimator animator;
    public int maxHealth = 100;
    int currentHealth;
    public int damageTaken;
    
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
        
        
        
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        animator.Die();

    }
}
