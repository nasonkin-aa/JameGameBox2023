using UnityEngine;

public class Spawner: MonoBehaviour
{
    private const float DelayTime = 2;

    private float _nextSpawnTime;
    
    public bool IsSpawnAllowed => Time.time >= _nextSpawnTime;
        
    public GameObject Spawn(GameObject enemy)
    {
        _nextSpawnTime = Time.time + DelayTime;
        return Instantiate(enemy, transform.position, Quaternion.identity);
    }
}