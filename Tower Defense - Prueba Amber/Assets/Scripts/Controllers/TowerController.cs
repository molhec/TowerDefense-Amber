using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    /// <summary>
    /// Array of Weapons to choose. Assigned on the edit in this order: 0-Pistol, 1-Rifle, 2-Shotgun
    /// </summary>
    [Header("Weapons")]
    [SerializeField] private WeaponController[] weapons;

    [SerializeField] private int towerHealth = 3;

    private int currentTowerHealth;
    private bool isDestroyed = false;

    private void Start()
    {
        // Subscribe to EventController Events
        EventsController.current.OnResetGame += ResetTower;
        
        currentTowerHealth = towerHealth;
        
        SelectNewWeapon(1);
    }

    private void OnDestroy()
    {
        EventsController.current.OnResetGame -= ResetTower;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if Enemy Collides with Tower and inflict Tower Damage
        if (other.CompareTag("Enemy") && !isDestroyed)
            ReceiveDamage(other);
    }
    
    /// <summary>
    /// Select a new weapon based on index
    /// </summary>
    /// <param name="indexWeaponToChoose">must be based on the order that are assigned on the weapons array</param>
    public void SelectNewWeapon(int indexWeaponToChoose)
    {
        foreach (var weapon in weapons)
            weapon.gameObject.SetActive(false);
        
        weapons[indexWeaponToChoose].gameObject.SetActive(true);
    }
    
    private void ReceiveDamage(Collider enemyCollider)
    {
        currentTowerHealth--;

        if (currentTowerHealth <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            EventsController.current.LoseGame();
        }
        
        EventsController.current.EnemyArriveToTower(enemyCollider);
    }

    private void ResetTower()
    {
        currentTowerHealth = towerHealth;
        isDestroyed = false;
    }
}
