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
        room.OnPlayerEnter.AddListener(CharacterEnter);
        spawnManager = GetComponentInChildren<SpawnManager>(); 
        character = FindObjectOfType<Character>();
        character.OnDie.AddListener(CharacterDie);
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
    public override void CharacterDie()
    {
        character.OnDie.RemoveListener(CharacterDie);
        OnCharacterDie.Invoke();
    }
    public override void CharacterEnter(int targetCount)
    {
        spawnManager.StartSpawn();    
        OnStart.Invoke(CountToTarget);
    }
    public override void LevelProgress(int count)
    {
        _targetNow++;
        OnLevelProgress.Invoke(_targetNow); 
    }

    public override void CharacterEnter()
    {
        spawnManager.StartSpawn();
        OnStart.Invoke(CountToTarget);
    }

    public override void StopLvl ()
    {
        character.Disable();
    }
}
