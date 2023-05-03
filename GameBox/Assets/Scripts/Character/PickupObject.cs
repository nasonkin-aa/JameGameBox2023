using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool isBeingCarried = false; // флаг, указывающий на то, что объект взят
    private Vector3 offset; // вектор смещения между положением курсора и объектом
    private Rigidbody2D rb; // ссылка на Rigidbody объекта, который будет перемещаться

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) // если нажата правая кнопка мыши
        {
            Debug.Log("1");
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)); // вычисляем смещение между курсором и объектом
            isBeingCarried = true; // устанавливаем флаг, указывающий на то, что объект взят
        }
    }

    void OnMouseDrag()
    {
        if (isBeingCarried) // если объект взят
        {
            Debug.Log("2");
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)) + offset; // вычисляем новую позицию объекта
            rb.MovePosition(newPosition); // перемещаем объект
        }
    }

    void OnMouseUp()
    {
        isBeingCarried = false; // сбрасываем флаг, указывающий на то, что объект взят
    }
}
