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

    public event Action<Vector3, Color> OnDrawWeaponTrajectory;
    public void DrawWeaponTrajectory(Vector3 worldPosMouse, Color trajectoryColor) => OnDrawWeaponTrajectory?.Invoke(worldPosMouse, trajectoryColor);
    
    public event Action<Collider, Bullet> OnDamageReceived;
    public void ReceiveDamage(Collider colliderToCheck, Bullet bullet) => OnDamageReceived?.Invoke(colliderToCheck, bullet);
    
    public event Action OnEnemyKilled;
    public void EnemyKilled() => OnEnemyKilled?.Invoke();
    
    public event Action OnEnemyArrivedToTower;
    public void EnemyArrivedToTower() => OnEnemyArrivedToTower?.Invoke();
    
    public event Action<int> OnUpdateRemainingZombies;
    public void UpdateRemainingZombies(int remainingZombies) => OnUpdateRemainingZombies?.Invoke(remainingZombies);
    
    public event Action OnWinGame;
    public void WinGame() => OnWinGame?.Invoke();
    
    public event Action OnLoseGame;
    public void LoseGame() => OnLoseGame?.Invoke();
    
    public event Action OnResetGame;
    public void ResetGame() => OnResetGame?.Invoke();
}
