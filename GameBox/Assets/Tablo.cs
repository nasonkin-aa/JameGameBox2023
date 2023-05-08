using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tablo : MonoBehaviour
{
    readonly List<Win> _win = new();
    readonly List<Lose> _lose = new();

    public UnityEvent OnWin;
    public UnityEvent OnLose;

    void Start()
    {
        _win.AddRange(GetComponentsInChildren<Win>(true));
        _lose.AddRange(GetComponentsInChildren<Lose>(true));
    }
    
    public void ActivateWin ()
    {
        _win[0].gameObject.SetActive(true);
        _win.Remove(_win[0]);
        if (_win.Count == 0)
            OnWin.Invoke();
    }

    public void ActivateLose()
    {
        _lose[0].gameObject.SetActive(true);
        _lose.Remove(_lose[0]);
        if (_lose.Count == 0)
            OnLose.Invoke();
    }
}
