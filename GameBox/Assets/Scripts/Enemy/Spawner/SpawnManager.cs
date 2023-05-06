using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class SpawnManager: MonoBehaviour
{
    public int numOfAllowedEnemies = 5;
    private int _counter;
    public UnityEvent OnFinishLevel;
    public UnityEvent<int> OnLevelProgress;

    public GameObject[] enemies;

    public GameObject[] spawners;
    
    private int _numOfSpawnedEnemies;
    public void Start()
    {
        _counter = numOfAllowedEnemies;
    }
    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }
    public void EnemyCounter(GameObject obj)
    {
        Debug.Log(_counter);
        _counter--;
        Debug.Log(_counter);
        obj.GetComponent<BaseEnemy>().OnDie.RemoveListener(EnemyCounter);
        OnLevelProgress.Invoke(_counter);
        if (_counter <= 0 ) 
        {
            OnFinishLevel.Invoke();
        }
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
            
            spawner.Spawn(enemy).GetComponent<BaseEnemy>().OnDie.AddListener(EnemyCounter);

            _numOfSpawnedEnemies += 1;

            yield return new WaitForSeconds(1);
        }
    }
}