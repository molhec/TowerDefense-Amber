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

    public int currentTowerHealth;
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
            ReceiveDamage();
    }

    public void ChangeWeaponToPistol()
    {
        pistol.gameObject.SetActive(true);
        rifle.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
    }
    
    public void ChangeWeaponToRifle()
    {
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(true);
        shotgun.gameObject.SetActive(false);
    }
    
    public void ChangeWeaponToShotgun()
    {
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(true);
    }

    private void ReceiveDamage()
    {
        currentTowerHealth--;

        if (currentTowerHealth <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            EventsController.current.LoseGame();
        }
        
        EventsController.current.EnemyArriveToTower();
    }

    private void ResetTower()
    {
        currentTowerHealth = towerHealth;
        isDestroyed = false;
    }
}
