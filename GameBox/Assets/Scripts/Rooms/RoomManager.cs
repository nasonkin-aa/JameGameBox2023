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
    }

    public GameObject SpawnRoom(int index)
    {
        var room = rooms[index - 1];
        
        var spawnPoint = GetSpawnPoint();
        _currRoom = Instantiate(room, spawnPoint, Quaternion.identity);
        Debug.Log(12121212);

        // TODO: почему нахуй блять
        var newY = hub.transform.position.y + _hubComponent.bounds.extents.y - _currRoomComponent.bounds.extents.y;
        _currRoom.transform.position = new Vector2(_currRoom.transform.position.x, newY);
        
        var newXPosition = hub.transform.position.x + _hubComponent.bounds.size.x / 2;

        StartCoroutine(MoveRoom(new Vector2(newXPosition, _currRoom.transform.position.y)));
        return _currRoom;
    }

    private Vector2 GetSpawnPoint()
    {
        var spawnX = hub.transform.position.x + _hubComponent.bounds.size.x + 10f;
        var spawnY = hub.transform.position.y;
        
        return new Vector2(spawnX, spawnY);
    }
    
    private IEnumerator MoveRoom(Vector2 targetPosition, Action action = null)
    {
        while (_currRoom.transform.position.x - _currRoomComponent.bounds.size.x / 2 > targetPosition.x)
        {
            _currRoom.transform.position = Vector2.MoveTowards(_currRoom.transform.position, targetPosition, 2f * Time.deltaTime);
            yield return null;
        }

        action?.Invoke();
    }
}