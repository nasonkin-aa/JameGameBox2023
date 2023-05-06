using UnityEngine;
using UnityEngine.Events;

public abstract class IRoomManagerBase : MonoBehaviour
{
    public UnityEvent<int> OnStart;
    public UnityEvent OnFinish;
    public UnityEvent OnCharacterDie;
    public UnityEvent OnLose;
    public UnityEvent<int> OnLevelProgress;
    public int CountToTarget = 20;
    protected int _targetNow = 0;
    public int TargetNow
    {
        get { return _targetNow; }
        set { _targetNow = value; }
    }

    public abstract void Failed();

    public abstract void Finished();

    public abstract void CharacterDie();

    public abstract void CharacterEnter( int targetCount);
    public abstract void CharacterEnter();

    public abstract void LevelProgress(int count);

}
