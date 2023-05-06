using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomManager: MonoBehaviour
{
    public GameObject[] rooms;
    
    private float _screenTop;
    private float _screenLeft;
    private float _screenRight;
    
    private GameObject _currRoom;
    
    public static RoomManager Instance { get; private set; }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DestroyRoom();
        }
    }

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

    public GameObject SpawnRoom(int index)
    {
        
        var room = rooms[index - 1];
        var roomSize = room.GetComponent<SpriteRenderer>().bounds.size;
        var spawnPoint = GetSpawnPoint(roomSize.x, roomSize.y);
        
        _currRoom = Instantiate(room, spawnPoint , Quaternion.identity);
        
        var targetPosition = new Vector2(spawnPoint.x, spawnPoint.y - 2f);
        StartCoroutine(MoveRoom(targetPosition));
        return _currRoom;
    }
    
    public void DestroyRoom()
    {
        if (_currRoom != null)
        {
            var roomSize = _currRoom.GetComponent<SpriteRenderer>().bounds.size;
            var targetPosition = GetSpawnPoint(roomSize.x, roomSize.y);
            StartCoroutine(MoveRoom(targetPosition, () => Destroy(_currRoom)));
        }
    }
    
    private Vector2 GetSpawnPoint(float roomWidth, float roomHeight)
    {
        var spawnX = (_screenLeft + _screenRight) / 2f;
        var spawnY = _screenTop + roomHeight / 2 + 1f;

        return new Vector2(spawnX, spawnY);
    }
    
    private IEnumerator MoveRoom(Vector2 targetPosition, Action action = null)
    {
        var currPosition = _currRoom.transform.position;

        while (currPosition.y != targetPosition.y)
        {
            currPosition = _currRoom.transform.position;
            _currRoom.transform.position = Vector2.MoveTowards(currPosition, targetPosition, 1f * Time.deltaTime);
            yield return null;
        }

        action?.Invoke();
    }
}