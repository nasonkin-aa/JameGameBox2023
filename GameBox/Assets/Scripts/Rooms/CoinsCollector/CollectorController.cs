using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorController : IRoomManagerBase
{
    private Character character;

    private void Start()
    {
        character = FindObjectOfType<Character>();
        character.OnDie.AddListener(CharacterDie);
        character.OnTakeCoin.AddListener(CoinCollected);
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
        OnFinish.Invoke();
    }

    public override void LevelProgress(int count)
    {
        OnLevelProgress.Invoke(count);
    }

    private void CoinCollected()
    {
        _targetNow++;
        LevelProgress(_targetNow);


        if (_targetNow == CountToTarget)
            Finished();
    }

    public override void StopLvl()
    {
        character.Disable();
    }
}
