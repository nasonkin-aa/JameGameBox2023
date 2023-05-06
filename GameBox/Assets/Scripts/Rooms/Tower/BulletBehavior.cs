using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 5f; // �������� �������
    public float lifespan = 2f; // ����� ����� ������� (� ��������)
    public int damage = 1; // ����, ������� ������� ������
    public Vector2 target;
    public GameObject AnimationExplosion;
    private float birthTime; // ����� �������� �������


    private void Start()
    {
        birthTime = Time.time; // ���������� ����� �������� �������
    }

    private void Update()
    {
        if ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime );
           
            float distance = Vector2.Distance(transform.position, target);
            float speedModifier = Mathf.Lerp(0.1f, 0.3f, distance / 10);
            if (transform.localScale.x > 0.1f)
                transform.localScale = new Vector3(speedModifier, speedModifier, speedModifier);

        }
        else
        {
            Instantiate(AnimationExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
