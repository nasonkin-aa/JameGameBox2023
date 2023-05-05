using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomManager: MonoBehaviour
{
    public GameObject[] rooms;

    private GameObject _currRoom;
    private float _screenTop;
    private float _screenLeft;
    private float _screenRight;
    
    public static RoomManager Instance { get; private set; }

    private void Awake()
    {
        var screenTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        var screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        _screenTop = screenTopLeft.y;
        _screenLeft = screenTopLeft.x;
        _screenRight = screenTopRight.x;
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void SpawnRoom(int index)
    {
        
        var room = rooms[index - 1];
        var roomSize = room.GetComponent<SpriteRenderer>().bounds.size;
        
        var spawnPoint = GetSpawnPoint(roomSize.x, roomSize.y);
        
        _currRoom = Instantiate(room, spawnPoint , Quaternion.identity);
        StartCoroutine(MoveRoom());
    }
    
    public void DestroyRoom()
    {
        if (_currRoom != null)
        {
            Destroy(_currRoom);
        }
    }
    
    private Vector2 GetSpawnPoint(float roomWidth, float roomHeight)
    {
        var spawnX = (_screenLeft + _screenRight) / 2f;
        var spawnY = _screenTop + roomHeight / 2 + 1f;

        return new Vector2(spawnX, spawnY);
    }
    
    private IEnumerator MoveRoom()
    {
        var roomSize = _currRoom.GetComponent<SpriteRenderer>().bounds.size;
        var currPosition = _currRoom.transform.position;
        var targetPosition = new Vector3(currPosition.x, _screenTop + roomSize.y / 2 - 1f, 0);

        while (currPosition.y > targetPosition.y)
        {
            currPosition = _currRoom.transform.position;
            _currRoom.transform.position = Vector3.MoveTowards(currPosition, targetPosition, 1f * Time.deltaTime);
            yield return null;
        }
    }
}