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
        EventsController.current.OnDamageReceived += ReceiveDamage;
        maxHealth = enemyData.currentHealth;
    }

    private void OnDestroy()
    {
        EventsController.current.OnDamageReceived -= ReceiveDamage;
    }

    public void StartNavMeshPath(Vector3 posToGo)
    {
        StartCoroutine(FollowingCoroutine(posToGo));
    }

    IEnumerator FollowingCoroutine(Vector3 posToGo)
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
        EventsController.current.EnemyArrivedToTower();
    }

    private void ReceiveDamage(Collider colliderToCheck, Bullet bullet)
    {
        if(isDead) return;
        
        if (colliderToCheck == headCollider) 
        {
            enemyData.currentHealth -= bullet.headshotDamage;
        }
        else if (colliderToCheck == bodyCollider)
        {
            enemyData.currentHealth -= bullet.bodyDamage;
        }
        
        float healthBarValue = enemyData.currentHealth / maxHealth;
        enemyView.UpdateHealthBar(healthBarValue);
        
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
