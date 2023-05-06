using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batton : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            Main.StartSpin();
        }
    }
}
