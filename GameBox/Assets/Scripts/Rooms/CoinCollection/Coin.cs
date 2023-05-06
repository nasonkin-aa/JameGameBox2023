using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CoinManagers
{
    private CoinManagers coinManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManagers>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>()) 
        {
            coinManager.AddCoins();
            Destroy(gameObject); 
        }
    }
}
