using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 mousePos;

    public float speedChar = 5f;

    public  Rigidbody2D rbChar;

    private void Start()
    {
        rbChar = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
      
        LookAt();

    }
    private void FixedUpdate()
    {
        PlayerControler();
    }
    private void PlayerControler()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1f;
        }

        movement.Normalize();

        rbChar.velocity = movement * speedChar;
    }


    private void LookAt()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10; 
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
