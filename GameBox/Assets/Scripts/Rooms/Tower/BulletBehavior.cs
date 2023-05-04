using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10f; // �������� �������
    public float lifespan = 2f; // ����� ����� ������� (� ��������)
    public int damage = 1; // ����, ������� ������� ������

    private float birthTime; // ����� �������� �������

    private void Start()
    {
        birthTime = Time.time; // ���������� ����� �������� �������
    }

    private void Update()
    {
        // ���������, �� ����� �� ���� ����� �������
        if (Time.time >= birthTime + lifespan)
        {
            Destroy(gameObject); // ���������� ������, ���� �� ������ ���� ���
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������� ������
        //Destroy(gameObject);
    }
}
