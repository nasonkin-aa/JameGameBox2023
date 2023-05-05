using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level instance;
    
    public List<GameObject> Levels;
    private GameObject spawnedObject;
    public static Level Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    public void Spawn(int index)
    {
        spawnedObject = Instantiate(Levels[index - 1]);
    }

    public void Delete()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
    }
}
