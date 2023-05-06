using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;

    private Vector3 _velocity = Vector3.zero;
    void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);
        var smooth = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        smooth.z = -10;
        transform.position = smooth;
    }
}