using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomWaves : IRoomManagerBase
{
    public SpawnManager spawnManager;
    public StarRoom room;
    public Character character;

    private void Start()
    {
        room = GetComponentInChildren<StarRoom>();
        room.OnPlayerEnter.AddListener(OnCharacterEnter);
        spawnManager = GetComponentInChildren<SpawnManager>(); 
        character = FindObjectOfType<Character>();
        character.OnDie.AddListener(OnCaracterDie);
        spawnManager.OnFinishLevel.AddListener(Finished);
    }
    public override void Failed()
    {
        OnLose.Invoke();
    }
    public override void Finished()
    {
        spawnManager.OnFinishLevel.RemoveListener(Finished);
        OnFinish.Invoke();
    }
    public override void OnCaracterDie()
    {
        character.OnDie.RemoveListener(OnCaracterDie);
        OnCharacterDie.Invoke();
    }
    public override void OnCharacterEnter()
    {
        spawnManager.StartSpawn();    
        OnStart.Invoke(CountToTarget);
    }
    public override void LevelProgress()
    {
        _targetNow++;
        OnLevelProgress.Invoke(_targetNow);
    }
}
