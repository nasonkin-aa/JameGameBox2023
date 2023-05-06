using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character player = collision.GetComponent<Character>();
        if (player)
            player.TakeCoin();
    }
}
