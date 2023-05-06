using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SaveZoneFollowers : MonoBehaviour
{
    public int CountFollowers;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FollowPlayer>())
        {
            CountFollowers++;
            Debug.Log(CountFollowers);
        }
    }
}
