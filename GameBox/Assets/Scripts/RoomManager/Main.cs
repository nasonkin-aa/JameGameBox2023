using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : IRoomManagerBase
{
    private IRoomManagerBase _newRoom;
    private TaskWheel _wheel;
    // Start is called before the first frame update
    void Start()
    {
        TaskWheel.OnRoomCreate.AddListener(OnCreateRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void StartSpin ()
    {
        TaskWheel.Spin();
    }

    public void OnCreateRoom(GameObject room)
    {
        _newRoom = room.GetComponent<IRoomManagerBase>();
        //_newRoom.OnStart.AddListener(OnCharacterEnter);
    }

    public override void Failed()
    {
        throw new System.NotImplementedException();
    }

    public override void Finished()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCaracterDie()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCharacterEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelProgress()
    {
        throw new System.NotImplementedException();
    }
}
