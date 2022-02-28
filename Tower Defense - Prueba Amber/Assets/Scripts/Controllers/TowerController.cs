using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private WeaponController pistol;
    [SerializeField] private WeaponController rifle;
    [SerializeField] private WeaponController shotgun;

    [SerializeField] private int towerHealth = 3;

    private int currentTowerHealth;
    private bool isDestroyed = false;

    private void Start()
    {
        // Subscribe to EventController Events
        EventsController.current.OnResetGame += ResetTower;
        
        // Initialize to default values

        currentTowerHealth = towerHealth;
        
        ChangeWeaponToRifle();
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
    /// Enables the pistol weapon and disable the other ones, invoked from Button UI Unity Event
    /// </summary>
    public void ChangeWeaponToPistol()
    {
        pistol.gameObject.SetActive(true);
        rifle.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Enables the rifle weapon and disable the other ones, invoked from Button UI Unity Event
    /// </summary>
    public void ChangeWeaponToRifle()
    {
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(true);
        shotgun.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Enables the shotgun weapon and disable the other ones, invoked from Button UI Unity Event
    /// </summary>
    public void ChangeWeaponToShotgun()
    {
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(true);
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
