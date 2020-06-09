using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public EnemyAI enemy;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        slider.value = enemy.currentHealth;
    }
}
