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
        Debug.Log(towerBehavior);
        towerBehavior.OnFinishLevel.AddListener(Finished);

    }
    public override void CharacterDie()
    {
        character.OnDie.RemoveListener(CharacterDie);
        OnCharacterDie.Invoke();
    }

    public override void CharacterEnter(int targetCount)
    {
        OnStart.Invoke(CountToTarget);
    }

    public override void CharacterEnter()
    {
        OnStart.Invoke(CountToTarget);
    }

    public override void Failed()
    {
        OnLose.Invoke();
    }

    public override void Finished()
    {
        towerBehavior.OnFinishLevel.RemoveListener(Finished);
        OnFinish.Invoke();
    }

    public override void LevelProgress(int count)
    {
        throw new System.NotImplementedException();
    }
}
