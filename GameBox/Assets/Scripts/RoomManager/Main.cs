using UnityEngine;

public class Main : IRoomManagerBase
{
    private IRoomManagerBase _newRoom;

    [SerializeField]
    protected TaskWheel _wheel;

    public GameObject _drone;

    protected static bool _isRoomStarted = false;


    void Start()
    {
        _wheel.OnRoomCreate.AddListener(CreateRoom);
    }

    public static void StartSpin ()
    {
        TaskWheel.Spin();
        _isRoomStarted = true;
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
        //Instantiate()
        RemoveAllEvents(_newRoom);
    }

    public override void Finished()
    {
        Debug.Log("finish");
        _isRoomStarted = false;
        RemoveAllEvents(_newRoom);
    }

    public override void CharacterDie()
    {
        Debug.Log("die");
        _isRoomStarted = false;
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
        Debug.Log("����� " + targetCount);
        throw new System.NotImplementedException();
    }

    public override void CharacterEnter()
    {
        Debug.Log("����� ������");
        throw new System.NotImplementedException();
    }

    public override void LevelProgress(int count)
    {
        Debug.Log("�������� " + count);
        throw new System.NotImplementedException();
    }


}
