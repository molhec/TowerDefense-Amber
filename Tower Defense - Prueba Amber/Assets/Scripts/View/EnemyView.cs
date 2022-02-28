using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    
    public void UpdateHealthBar(float newHealthBarValue)
    {
        if(newHealthBarValue > 0)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = newHealthBarValue;
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
    }
}
