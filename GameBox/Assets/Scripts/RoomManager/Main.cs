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
    [SerializeField]
    protected Tablo _tablo;

    protected bool _isRoomStarted = false;
    protected GameObject _currentRoom;


    void Start()
    {
        _button.OnButtonClick.AddListener(StartSpin);
        _wheel.OnRoomCreate.AddListener(CreateRoom);
        _tablo.OnWin.AddListener(GameOver);
        _tablo.OnLose.AddListener(GameOver);
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
        EndOfLvl();
        _tablo.ActivateLose();
    }

    public override void Finished()
    {
        Debug.Log("finish");
        EndOfLvl();
        _tablo.ActivateWin();
    }

    public override void CharacterDie()
    {
        Debug.Log("die");
        EndOfLvl();
        _tablo.ActivateLose();
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
            _currentRoom.transform.position = Vector2.MoveTowards(
                _currentRoom.transform.position, 
                new Vector2(targetPositionX, _currentRoom.transform.position.y),
                5f * Time.deltaTime);
            yield return null;
        }
        Destroy(_currentRoom);
    }

    public void GameOver()
    {
        Debug.Log("Игра окончена");
    }

    private void PlayerTaken()
    {
        StartCoroutine(MoveRoomAway(_currentRoom.transform.position.x + 20));
    }

    private void EndOfLvl()
    {
        _isRoomStarted = false;
        var drone = Instantiate(_drone, transform.position, transform.rotation);
        drone.GetComponent<DroneController>().OnPlayereTaken.AddListener(PlayerTaken);
        StopLvl();

        RemoveAllEvents(_newRoom);
    }

    public override void StopLvl()
    {
        _newRoom.StopLvl();
    }
}
