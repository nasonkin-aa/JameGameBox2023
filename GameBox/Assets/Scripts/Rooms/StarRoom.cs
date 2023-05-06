using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarRoom : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            OnPlayerEnter.Invoke();
        }
    }
}
