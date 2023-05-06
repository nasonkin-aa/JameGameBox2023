using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBall : MonoBehaviour
{

    private Rigidbody2D rb;
    public float slowDownForce = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (rb.velocity.magnitude > 0.01f)
        {
            Vector2 oppositeForce = -rb.velocity.normalized * slowDownForce;
            rb.AddForce(oppositeForce);
        }
       // Debug.Log(rb.velocity.magnitude);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BaseEnemy>() && rb.velocity.magnitude >= 7)
        {
            collision.gameObject.GetComponent<BaseEnemy>().GetDamage(1);
        }
    }



}
