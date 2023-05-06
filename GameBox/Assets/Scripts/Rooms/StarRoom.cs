using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarRoom : MonoBehaviour
{
    public GameObject door;
    public UnityEvent OnPlayerEnter;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            door.SetActive(true);
            OnPlayerEnter.Invoke();
        }
    }
}
