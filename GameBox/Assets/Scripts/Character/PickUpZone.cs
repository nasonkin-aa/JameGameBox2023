using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpZone : MonoBehaviour
{
    private bool isBeingCarried = false; // флаг, указывающий на то, что объект взят
    private Rigidbody2D rbBall; // ссылка на Rigidbody объекта, который будет перемещаться
    public float pushSpeed = 10f;
    public GameObject Char;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ChainBall>() && Input.GetMouseButton(0)) 
        {
            Debug.Log("isball");
            rbBall = collision.GetComponent<Rigidbody2D>();
            isBeingCarried = true;
            if (Input.GetMouseButton(1))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = mousePosition - (Vector2)collision.transform.position;
                direction.Normalize();
                collision.GetComponent<Rigidbody2D>().velocity = direction * pushSpeed;
                //Char.GetComponent<Rigidbody2D>().velocity = direction * pushSpeed;
                /*Vector2 charDirection = mousePosition - (Vector2)Char.transform.position;
                charDirection.Normalize();
                Char.GetComponent<Rigidbody2D>().MovePosition((Vector2)Char.transform.position + charDirection * pushSpeed);*/
               /* direction = mousePosition - (Vector2)Char.transform.position;
                direction.Normalize();*/
                StartCoroutine(MoveCharacter(Char.transform.position + (collision.transform.position  - Char.transform.position) * 3 ));

                isBeingCarried = false;
            }
        }
        else
        {
            isBeingCarried = false;
        }
        
    }
    IEnumerator MoveCharacter(Vector2 targetPosition)
    {
        float time = 0f;
        Vector3 startPosition = Char.transform.position;
        yield return new WaitForSeconds(0.2f);
        while (time < 1f)
        {
            time += Time.deltaTime * 5 ;
            Char.transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }
    }
    private void Update()
    {
        if (isBeingCarried) // если объект взят
        {
            Debug.Log("2");
            Vector3 newPosition = transform.position; // вычисляем новую позицию объекта
            rbBall.MovePosition(newPosition); // перемещаем объект
        }
    }
}
