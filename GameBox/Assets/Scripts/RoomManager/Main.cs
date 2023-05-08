using System.Collections;
using UnityEngine;

public class Main : IRoomManagerBase
{
    public IRoomManagerBase _newRoom;

    [SerializeField]
    protected TaskWheel _wheel;
    [SerializeField]
    protected GameObject _drone;
    [SerializeField]
    protected Button _button;

    protected bool _isRoomStarted = false;
    protected GameObject _currentRoom;


    void Start()
    {
        _button.OnButtonClick.AddListener(StartSpin);
        _wheel.OnRoomCreate.AddListener(CreateRoom);
    }

    public void StartSpin()
    {
        Debug.Log("spawn");
        if (!_isRoomStarted)
        {
            _isRoomStarted = true;
            TaskWheel.Spin();
        }
    }

    public void CreateRoom(GameObject room)
    {
        _newRoom = room.GetComponentInChildren<IRoomManagerBase>();
        _newRoom.OnStart.AddListener(CharacterEnter);
        _newRoom.OnFinish.AddListener(Finished);
        _newRoom.OnCharacterDie.AddListener(CharacterDie);
        _newRoom.OnLevelProgress.AddListener(LevelProgress);
        _newRoom.OnLose.AddListener(Failed);
        _currentRoom = room;
    }

    public override void Failed()
    {
        Debug.Log("lose");
        _isRoomStarted = false;
        Instantiate(_drone, transform.position, transform.rotation);

        RemoveAllEvents(_newRoom);
        StartCoroutine(MoveRoomAway(_currentRoom.transform.position.x + 30));
    }

    public override void Finished()
    {
        Debug.Log("finish");
        _isRoomStarted = false;
        Instantiate(_drone, transform.position, transform.rotation);

        RemoveAllEvents(_newRoom);
        StartCoroutine(MoveRoomAway(_currentRoom.transform.position.x + 30));
    }

    public override void CharacterDie()
    {
        Debug.Log("die");
        _isRoomStarted = false;
        Instantiate(_drone, transform.position, transform.rotation);

        RemoveAllEvents(_newRoom);
        StartCoroutine(MoveRoomAway(_currentRoom.transform.position.x + 30));
    }

    public void RemoveAllEvents(IRoomManagerBase room)
    {
        room.OnStart.RemoveAllListeners();
        room.OnFinish.RemoveAllListeners();
        room.OnCharacterDie.RemoveAllListeners();
        room.OnLevelProgress.RemoveAllListeners();
        room.OnLose.RemoveAllListeners();
    }

    public override void CharacterEnter(int targetCount)
    {
        Debug.Log("зашёл " + targetCount);
    }

    public override void CharacterEnter()
    {
        Debug.Log("зашёл просто");
    }

    public override void LevelProgress(int count)
    {
        Debug.Log("прогресс " + count);

    }

    private IEnumerator MoveRoomAway(float targetPositionX)
    {
        while (_currentRoom.transform.position.x < targetPositionX)
        {
            _currentRoom.transform.position = Vector2.MoveTowards(_currentRoom.transform.position, new Vector2(targetPositionX, _currentRoom.transform.position.y), 2f * Time.deltaTime);
            yield return null;
        }
        Destroy(_currentRoom);
    }
}
