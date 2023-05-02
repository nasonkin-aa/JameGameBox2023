using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 mousePos;

    public float speedChar = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement.normalized * speedChar;
        /* float moveHorizontal = Input.GetAxis("Horizontal");
         float moveVertical = Input.GetAxis("Vertical");

         Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);

         rb.MovePosition(transform.position + movement * speed * Time.deltaTime);*/
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
