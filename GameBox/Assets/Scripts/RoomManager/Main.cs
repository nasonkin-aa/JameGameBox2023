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


    void Start()
    {
        _button.OnButtonClick.AddListener(StartSpin);
        _wheel.OnRoomCreate.AddListener(CreateRoom);
        Debug.Log(_wheel);
    }

    public void StartSpin ()
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
    }

    public override void Failed()
    {
        Debug.Log("lose");
        _isRoomStarted = false;
        Debug.Log(Instantiate(_drone, transform.position, transform.rotation));
        RemoveAllEvents(_newRoom);
    }

    public override void Finished()
    {
        Debug.Log("finish");
        _isRoomStarted = false;
        Debug.Log(Instantiate(_drone, transform.position, transform.rotation));
        RemoveAllEvents(_newRoom);
    }

    public override void CharacterDie()
    {
        Debug.Log("die");
        _isRoomStarted = false;
        Debug.Log(Instantiate(_drone, transform.position, transform.rotation));

        RemoveAllEvents(_newRoom);
    }

    public void RemoveAllEvents (IRoomManagerBase room)
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
        //Debug.Log(Instantiate(_drone, transform.position, transform.rotation));
    }

    public override void CharacterEnter()
    {
        Debug.Log("зашёл просто");
        //Debug.Log(Instantiate(_drone, transform.position, transform.rotation));
    }

    public override void LevelProgress(int count)
    {
        Debug.Log("прогресс " + count);
        //Debug.Log(Instantiate(_drone, transform.position, transform.rotation));
    }


}
