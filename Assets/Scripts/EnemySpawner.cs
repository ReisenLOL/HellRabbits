using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyFolder;
    public Transform spawnLocation;
    public Enemy enemyToSpawn;
    public float spawnRate;
    public float currentSpawnTime;

    private void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime > spawnRate)
        {
            currentSpawnTime -= spawnRate;
            Enemy newEnemy = Instantiate(enemyToSpawn, enemyFolder);
            newEnemy.transform.position = spawnLocation.position;
        }
    }
}
