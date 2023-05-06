using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Bounds bounds;

    private Transform square;
    
    private void Awake()
    {
        var grid = transform.GetChild(0);

        if (grid)
        {
            square = grid.GetChild(0);
            bounds = square.GetComponent<Renderer>().bounds;
        }
    }
}
