using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagers : MonoBehaviour
{
    public int PickupedCoins = 0;

    public void AddCoins()
    {
        PickupedCoins += 1;
    }
}
