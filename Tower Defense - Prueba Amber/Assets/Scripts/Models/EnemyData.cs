using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private Enemy baseData;
    
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentSpeed;

    private void Awake()
    {
        currentHealth = baseData.health;
        currentSpeed = baseData.speed;
    }
}
