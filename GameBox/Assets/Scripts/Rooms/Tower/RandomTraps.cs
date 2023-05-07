using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTraps : MonoBehaviour
{
    public GameObject objectWarning;
    public GameObject objectTrap;
    public float spawnInterval = 5f;

    private int CountTraps = 20;
    private List<Vector2> WarningsPos = new List<Vector2>();
    private Collider2D spawnArea;
    private bool isSpawning = false;

    private void Start()
    {
        spawnArea = GetComponent<Collider2D>();
        Debug.Log(spawnArea);
        StartSpawning();
    }
    private void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnObjects());
    }

    private void StopSpawning()
    {
        isSpawning = false;
        StopCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (isSpawning)
        {
            // Спавним CountTraps объектов objectWarning в случайных местах внутри коллайдера
            for (int i = 0; i < CountTraps; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();
                
                GameObject newObj = Instantiate(objectWarning, spawnPosition, Quaternion.identity);
                WarningsPos.Add(spawnPosition);
                Destroy(newObj, 2f);
            }

            yield return new WaitForSeconds(2f);
            SpawnTraps(WarningsPos);

            yield return new WaitForSeconds(spawnInterval );
        }
    }

    private void SpawnTraps(List<Vector2> objects)
    {
        foreach (Vector2 obj in objects)
        {
            GameObject trap = Instantiate(objectTrap, obj, Quaternion.identity);
            Destroy(trap, 2f);
        }
        WarningsPos.Clear();
    }   

    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        return new Vector2(x, y);
    }
}
