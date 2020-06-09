using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerCombat player;
    public Slider slider;

    private void Update()
    {
        slider.value = player.currentHealth;
    }
}
