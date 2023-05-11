using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTower : IRoomManagerBase
{
    public TowerBehavior towerBehavior;
    public StarRoom room;
    public Character character;

    private void Start()
    {
        room = GetComponentInChildren<StarRoom>();
        room.OnPlayerEnter.AddListener(CharacterEnter);
        character = FindObjectOfType<Character>();
        
        character.OnDie.AddListener(CharacterDie);
        towerBehavior.OnFinishLevel.AddListener(Finished);

    }
    public override void CharacterDie()
    {
        Debug.Log("2");
        OnCharacterDie.Invoke();
        character.OnDie.RemoveListener(CharacterDie);
    }

    public override void CharacterEnter(int targetCount)
    {
        Debug.Log("3");
        OnStart.Invoke(CountToTarget);
    }

    public override void CharacterEnter()
    {
        Debug.Log("4");
        OnStart.Invoke(CountToTarget);
    }

    public override void Failed()
    {
        Debug.Log("5");
        OnLose.Invoke();
    }

    public override void Finished()
    {
        Debug.Log("6");
        OnFinish.Invoke();
        towerBehavior.OnFinishLevel.RemoveListener(Finished);
    }

    public override void LevelProgress(int count)
    {
        OnLevelProgress.Invoke(count);
    }

    public override void StopLvl()
    {
        character.Disable();
    }
}
