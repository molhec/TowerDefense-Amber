using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    public static EventsController current;

    private void Awake()
    {
        current = this;
    }
    
    // Methods and Actions that objects will access to comunicate to each other
    
    public event Action<Vector3, Color> OnDrawWeaponTrajectory;
    /// <summary>
    /// Gets the wouldPosMouse from the calculation and pass it for Drawing
    /// </summary>
    /// <param name="worldPosMouse">Also the hitPoint of the Raycast calculation</param>
    /// <param name="trajectoryColor">To update the color of the line renderer of the weapon</param>
    public void DrawWeaponTrajectory(Vector3 worldPosMouse, Color trajectoryColor) => OnDrawWeaponTrajectory?.Invoke(worldPosMouse, trajectoryColor);
    
    public event Action<Collider, Bullet> OnDamageReceived;
    /// <summary>
    /// Used in the EnemyController to Calculate and Apply the Damage
    /// </summary>
    /// <param name="colliderToCheck">Collider that hit the Raycast</param>
    /// <param name="bullet">Bullet of the weapon used</param>
    public void ReceiveDamage(Collider colliderToCheck, Bullet bullet) => OnDamageReceived?.Invoke(colliderToCheck, bullet);
    
    public event Action OnEnemyKilled;
    public void EnemyKilled() => OnEnemyKilled?.Invoke();
    
    public event Action<Collider> OnEnemyArrivedToTower;
    public void EnemyArriveToTower(Collider enemyCollider) => OnEnemyArrivedToTower?.Invoke(enemyCollider);

    public event Action<int> OnUpdateRemainingZombies;
    public void UpdateRemainingZombies(int remainingZombies) => OnUpdateRemainingZombies?.Invoke(remainingZombies);
    
    public event Action OnWinGame;
    public void WinGame() => OnWinGame?.Invoke();
    
    public event Action OnLoseGame;
    public void LoseGame() => OnLoseGame?.Invoke();
    
    public event Action OnResetGame;

    public void ResetGame() => OnResetGame?.Invoke();
}
