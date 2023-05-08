using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomManager: MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject hub;
    
    private GameObject _currRoom;

    private Transform _currRoomConPoint => _currRoom.GetComponentInChildren<ConnectPoint>().transform;
    private Vector2 _hubConPoint => hub.GetComponentInChildren<ConnectPoint>().transform.position;

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
        var room = rooms[index];
        
        var spawnPoint = GetSpawnPoint();
        _currRoom = Instantiate(room, spawnPoint, Quaternion.identity);

        var spawnX = _currRoom.transform.position.x;
        var spawnY = _hubConPoint.y - _currRoomConPoint.localPosition.y;
        _currRoom.transform.position = new Vector3(spawnX, spawnY);

        StartCoroutine(MoveRoom(new Vector2(_hubConPoint.x, _currRoom.transform.position.y)));
        return _currRoom;
    }

    private Vector2 GetSpawnPoint()
    {
        var spawnX = _hubConPoint.x + 30;
        var spawnY = _hubConPoint.y;
        
        return new Vector2(spawnX, spawnY);
    }
    
    private IEnumerator MoveRoom(Vector2 targetPosition, Action action = null)
    {
        while (_currRoomConPoint.position.x > targetPosition.x)
        {
            _currRoom.transform.position = Vector2.MoveTowards(_currRoom.transform.position, targetPosition, 2f * Time.deltaTime);
            yield return null;
        }

        action?.Invoke();
    }
}