using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SpawnManager: MonoBehaviour
{
    public int numOfAllowedEnemies = 5;

    public GameObject[] enemies;

    public GameObject[] spawners;
    
    private int _numOfSpawnedEnemies;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while(numOfAllowedEnemies > _numOfSpawnedEnemies)
        {
            var randSpawnIndex = new Random().Next(spawners.Length);
            var spawner = spawners[randSpawnIndex].GetComponent<Spawner>();

            while (!spawner.IsSpawnAllowed)
            {
                randSpawnIndex = new Random().Next(spawners.Length);
                spawner = spawners[randSpawnIndex].GetComponent<Spawner>();
                
                yield return null;
            }
            
            var randEnemyIndex = new Random().Next(enemies.Length);
            var enemy = enemies[randEnemyIndex];
            
            spawner.Spawn(enemy);
            _numOfSpawnedEnemies += 1;

            yield return new WaitForSeconds(1);
        }
    }
}