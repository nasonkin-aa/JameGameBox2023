using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level instance;
    
    private float screenTop;
    private float screenLeft;
    private float screenRight;
    
    public List<GameObject> Levels;
    private GameObject spawnedObject;
    public static Level Instance { get; private set; }
    private void Awake() 
    {
        var screenTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        var screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        screenTop = screenTopLeft.y;
        screenLeft = screenTopLeft.x;
        screenRight = screenTopRight.x;
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void FixedUpdate()
    {
        if (spawnedObject)
        {
            MoveRoom(spawnedObject);
        }
    }

    public void Spawn(int index)
    {
        var room = Levels[index - 1];
        var roomSize = room.GetComponent<SpriteRenderer>().bounds.size;
        
        var spawnPoint = GetSpawnPoint(roomSize.x, roomSize.y);
        
        spawnedObject = Instantiate(Levels[index - 1], spawnPoint , Quaternion.identity);
    }

    public void Delete()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
    }

    private Vector2 GetSpawnPoint(float roomWidth, float roomHeight)
    {
        var spawnX = (screenLeft + screenRight) / 2f;
        var spawnY = screenTop + roomHeight / 2 + 1f;

        return new Vector2(spawnX, spawnY);
    }
    
    private void MoveRoom(GameObject obj)
    {
        var roomSize = obj.GetComponent<SpriteRenderer>().bounds.size;
        
        Vector3 targetPosition = new Vector3(obj.transform.position.x, screenTop + roomSize.y / 2 - 1f, 0);
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, 1f * Time.deltaTime);
    }
}
