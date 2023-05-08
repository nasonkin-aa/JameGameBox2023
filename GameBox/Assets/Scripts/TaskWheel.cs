using System;
using UnityEngine;
using UnityEngine.Events;

public class TaskWheel: MonoBehaviour
{
    private const float Damping = 0.35f;
    
    private const float MinSpinSpeed = 40.0f;
    
    static float _speed;

    private float _spinAngle;

    static bool _taskWasGet = true;
    private bool IsSpinningAvailable => _speed >= MinSpinSpeed;

    public UnityEvent<GameObject> OnRoomCreate;

    public static void Spin()
    {
        _taskWasGet = false;
        _speed = new System.Random().Next(350, 550);
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
                _taskWasGet = true;
                var taskIndex = GetTaskIndex(transform.eulerAngles.z);

                OnRoomCreate.Invoke(RoomManager.Instance.SpawnRoom(taskIndex));
                
            }
        }
    }
}