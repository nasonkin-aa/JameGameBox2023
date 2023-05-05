using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Level.Instance.Delete();
            Level.Instance.Spawn(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Level.Instance.Delete();
            Level.Instance.Spawn(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Level.Instance.Delete();
            Level.Instance.Spawn(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Level.Instance.Delete();
            Level.Instance.Spawn(4);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Level.Instance.Delete();
        }
    }
}
