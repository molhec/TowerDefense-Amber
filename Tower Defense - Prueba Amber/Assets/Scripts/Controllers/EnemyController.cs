using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private EnemyView enemyView;
    [SerializeField] private NavMeshAgent navMeshAgentAgent;
    [SerializeField] private Animator anim;

    [SerializeField] private Collider headCollider;
    [SerializeField] private Collider bodyCollider;

    private float maxHealth;

    private bool isDead = false;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int Death = Animator.StringToHash("death");

    private void Start()
    {
        // Subscribe to EventController Events
        EventsController.current.OnDamageReceived += ReceiveDamage;
        EventsController.current.OnEnemyArrivedToTower += DeactivateColliders;
        maxHealth = enemyData.currentHealth;
    }

    private void OnDestroy()
    {
        // Unsubscribe to EventController Events
        EventsController.current.OnDamageReceived -= ReceiveDamage;
        EventsController.current.OnEnemyArrivedToTower -= DeactivateColliders;
    }
    
    /// <summary>
    /// Starts a coroutine to assing a position to go and updates the animation
    /// </summary>
    /// <param name="posToGo">Position where the enemy will go</param>
    public void StartNavMeshPath(Vector3 posToGo)
    {
        // Coroutine to make the Enemy to follow a path
        StartCoroutine(FollowingCoroutine(posToGo));
    }

    private void DeactivateColliders(Collider enemyCollider)
    {
        if (enemyCollider != headCollider && enemyCollider != bodyCollider) return;
        headCollider.gameObject.SetActive(false);
        bodyCollider.gameObject.SetActive(false);
    }

    private IEnumerator FollowingCoroutine(Vector3 posToGo)
    {
        navMeshAgentAgent.SetDestination(posToGo);
        navMeshAgentAgent.speed = enemyData.currentSpeed;
        anim.SetBool(IsWalking, true);

        while (!(Vector3.Distance(navMeshAgentAgent.destination, navMeshAgentAgent.transform.position) <=
                   navMeshAgentAgent.stoppingDistance && (navMeshAgentAgent.hasPath || navMeshAgentAgent.velocity.sqrMagnitude == 0f)))
        {
            yield return null;
        }
        
        anim.SetBool(IsWalking, false);
    }

    private void ReceiveDamage(Collider colliderToCheck, Bullet bullet)
    {
        // Calculate damage based on where the bullet hits the enemy
        
        if(isDead) return;
        
        if (colliderToCheck == headCollider) 
        {
            enemyData.currentHealth -= bullet.headshotDamage;
        }
        else if (colliderToCheck == bodyCollider)
        {
            enemyData.currentHealth -= bullet.bodyDamage;
        }
        
        // Update healthBar
        float healthBarValue = enemyData.currentHealth / maxHealth;
        enemyView.UpdateHealthBar(healthBarValue);
        
        // Check if the enemy gets killed
        
        if (enemyData.currentHealth <= 0)
        {
            isDead = true;
            navMeshAgentAgent.speed = 0f;
            anim.SetTrigger(Death);
            
            EventsController.current.EnemyKilled();

            StartCoroutine(DeactivateEnemy());
        }
    }

    private IEnumerator DeactivateEnemy()
    {
        yield return new WaitForSeconds(2.7f);
        gameObject.SetActive(false);
    }
}
