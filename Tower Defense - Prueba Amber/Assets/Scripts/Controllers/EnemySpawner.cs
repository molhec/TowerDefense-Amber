using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemiesToInstantiate;
    [SerializeField] private EnemyController[] enemiesPrefabs;
    [SerializeField] private Vector3 minSpawnZone;
    [SerializeField] private Vector3 maxSpawnZone;

    private List<EnemyController> poolEnemies = new List<EnemyController>();
    private int remainEnemiesToInstantiate;
    private int remainEnemiesToKill;

    private Coroutine spawnEnemiesCoroutine;

    public void Start()
    {
        EventsController.current.OnEnemyKilled += UpdateRemainingZombies;
        EventsController.current.OnResetGame += ResetSpawner;
        
        SpawnEnemies();
    }
    
    private void OnDestroy()
    {
        EventsController.current.OnEnemyKilled -= UpdateRemainingZombies;
        EventsController.current.OnResetGame -= ResetSpawner;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemiesToInstantiate; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(minSpawnZone.x, maxSpawnZone.x),
                Random.Range(minSpawnZone.y, maxSpawnZone.y),
                Random.Range(minSpawnZone.z, maxSpawnZone.z));
            
            poolEnemies.Add(Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length - 1)],randomPos, Quaternion.Euler(0f, 180f, 0f), transform));
        }

        foreach (var enemy in poolEnemies)
            enemy.gameObject.SetActive(false);

        remainEnemiesToInstantiate = poolEnemies.Count - 1;

        remainEnemiesToKill = remainEnemiesToInstantiate;
        
        EventsController.current.UpdateRemainingZombies(remainEnemiesToInstantiate);
        
        if(spawnEnemiesCoroutine != null)
            StopCoroutine(spawnEnemiesCoroutine);

        spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
    }
    
    private void UpdateRemainingZombies()
    {
        remainEnemiesToKill--;
        EventsController.current.UpdateRemainingZombies(remainEnemiesToKill);

        if (remainEnemiesToKill <= 0)
            EventsController.current.WinGame();
    }

    private void ResetSpawner()
    {
        foreach (var enemy in poolEnemies)
            Destroy(enemy);
        
        poolEnemies.Clear();
        
        SpawnEnemies();
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (remainEnemiesToInstantiate > 0)
        {
            poolEnemies[remainEnemiesToInstantiate].gameObject.SetActive(true);
            poolEnemies[remainEnemiesToInstantiate].StartNavMeshPath(transform.position);

            remainEnemiesToInstantiate--;

            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }
}
