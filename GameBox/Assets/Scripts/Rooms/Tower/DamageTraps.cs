using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTraps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            Debug.Log("21");
            collision.GetComponent<Character>().GetDamage(1);
        }
    }
}
