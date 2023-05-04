using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10f; // скорость снаряда
    public float lifespan = 2f; // время жизни снаряда (в секундах)
    public int damage = 1; // урон, который наносит снаряд

    private float birthTime; // время создания снаряда

    private void Start()
    {
        birthTime = Time.time; // запоминаем время создания снаряда
    }

    private void Update()
    {
        // проверяем, не истек ли срок жизни снаряда
        if (Time.time >= birthTime + lifespan)
        {
            Destroy(gameObject); // уничтожаем снаряд, если он прожил свой век
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // уничтожаем снаряд
        //Destroy(gameObject);
    }
}
