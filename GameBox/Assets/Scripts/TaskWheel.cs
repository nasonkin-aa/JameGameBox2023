using System;
using UnityEngine;

public class TaskWheel: MonoBehaviour
{
    private const float Damping = 0.25f;
    
    private const float MinSpinSpeed = 5.0f;
    
    private float _speed;

    private float _spinAngle;

    private bool _taskWasGet;
    private bool IsSpinningAvailable => _speed >= MinSpinSpeed;

    public void Spin()
    {
        _taskWasGet = false;
        _speed = new System.Random().Next(250, 350);
    }

    private void TurnTheWheel()
    {
        _spinAngle += _speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _spinAngle);

        _speed = Mathf.Lerp(_speed, 0.0f, Damping * Time.deltaTime);
    }

    private int GetTaskIndex(float angle)
    {
        // кол-во заданий
        var numberOfSections = 7;
        float angleOffset = 360 / numberOfSections;

        return Mathf.CeilToInt(((angle + angleOffset / 2) / angleOffset));
    }
    
    private void FixedUpdate()
    {
        if (IsSpinningAvailable)
        {
            TurnTheWheel();
        }
        else
        {
            if (!_taskWasGet)
            {
                var taskIndex = GetTaskIndex(transform.eulerAngles.z);
                print(taskIndex);
                
                RoomManager.Instance.SpawnRoom(taskIndex);
                _taskWasGet = true;
            }
        }
    }

    private void Start()
    {
        Spin();
    }
}