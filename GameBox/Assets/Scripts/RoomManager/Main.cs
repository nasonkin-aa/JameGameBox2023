using UnityEngine;

public class Main : IRoomManagerBase
{
    private IRoomManagerBase _newRoom;

    void Start()
    {
        TaskWheel.OnRoomCreate.AddListener(CreateRoom);
    }

    public static void StartSpin ()
    {
        TaskWheel.Spin();
    }

    public void CreateRoom(GameObject room)
    {
        _newRoom = room.GetComponent<IRoomManagerBase>();
        _newRoom?.OnStart.AddListener(CharacterEnter);
        _newRoom?.OnFinish.AddListener(Finished);
        _newRoom?.OnCharacterDie.AddListener(CharacterDie);
        _newRoom?.OnLevelProgress.AddListener(LevelProgress);
        _newRoom?.OnLose.AddListener(Failed);
    }

    public override void Failed()
    {
        RemoveAllEvents(_newRoom);
    }

    public override void Finished()
    {
        RemoveAllEvents(_newRoom);
    }

    public override void CharacterDie()
    {
        RemoveAllEvents(_newRoom);
    }

    public void RemoveAllEvents (IRoomManagerBase room)
    {
        room?.OnStart.RemoveAllListeners();
        room?.OnFinish.RemoveAllListeners();
        room?.OnCharacterDie.RemoveAllListeners();
        room?.OnLevelProgress.RemoveAllListeners();
        room?.OnLose.RemoveAllListeners();
    }

    public override void CharacterEnter(int targetCount)
    {
        throw new System.NotImplementedException();
    }

    public override void CharacterEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelProgress(int count)
    {
        throw new System.NotImplementedException();
    }
}
