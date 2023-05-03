using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool isBeingCarried = false; // ����, ����������� �� ��, ��� ������ ����
    private Vector3 offset; // ������ �������� ����� ���������� ������� � ��������
    private Rigidbody2D rb; // ������ �� Rigidbody �������, ������� ����� ������������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) // ���� ������ ������ ������ ����
        {
            Debug.Log("1");
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)); // ��������� �������� ����� �������� � ��������
            isBeingCarried = true; // ������������� ����, ����������� �� ��, ��� ������ ����
        }
    }

    void OnMouseDrag()
    {
        if (isBeingCarried) // ���� ������ ����
        {
            Debug.Log("2");
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)) + offset; // ��������� ����� ������� �������
            rb.MovePosition(newPosition); // ���������� ������
        }
    }

    void OnMouseUp()
    {
        isBeingCarried = false; // ���������� ����, ����������� �� ��, ��� ������ ����
    }
}
