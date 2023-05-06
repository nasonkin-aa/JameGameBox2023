using UnityEngine;

public class Spawner: MonoBehaviour
{
    private const float DelayTime = 2;

    private float _nextSpawnTime;
    
    public bool IsSpawnAllowed => Time.time >= _nextSpawnTime;
        
    public void Spawn(GameObject enemy)
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        _nextSpawnTime = Time.time + DelayTime;
    }
}