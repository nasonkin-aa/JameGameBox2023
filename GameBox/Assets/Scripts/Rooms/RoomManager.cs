using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomManager: MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject hub;
    
    private GameObject _currRoom;

    private Room _hubComponent => hub.GetComponent<Room>();
    private Room _currRoomComponent => _currRoom.GetComponent<Room>();
    
    public static RoomManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
        
        SpawnRoom(1);
    }

    public GameObject SpawnRoom(int index)
    {
        var room = rooms[index - 1];
        
        var spawnPoint = GetSpawnPoint();
        _currRoom = Instantiate(room, spawnPoint, Quaternion.identity);

        // TODO: почему нахуй блять
        var newY = _hubComponent.position.y;
        _currRoomComponent.position = new Vector2(_currRoomComponent.position.x, newY);
        
        var targetRightEdge = _hubComponent.bounds.max.x;
        var newXPosition = targetRightEdge + _currRoomComponent.bounds.size.x / 2;
        
        StartCoroutine(MoveRoom(new Vector2(newXPosition, _currRoomComponent.position.y)));
        return _currRoom;
    }

    private Vector2 GetSpawnPoint()
    {
        var spawnX = _hubComponent.position.x + _hubComponent.bounds.size.x + 10f;
        var spawnY = _hubComponent.position.y;
        
        return new Vector2(spawnX, spawnY);
    }
    
    private IEnumerator MoveRoom(Vector2 targetPosition, Action action = null)
    {
        var currPosition = _currRoomComponent.position;

        while (currPosition.x != targetPosition.x)
        {
            currPosition = _currRoomComponent.position;
            _currRoomComponent.position = Vector2.MoveTowards(currPosition, targetPosition, 2f * Time.deltaTime);
            yield return null;
        }

        action?.Invoke();
    }
}