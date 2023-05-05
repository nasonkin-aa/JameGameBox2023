using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 5f; // скорость снаряда
    public float lifespan = 2f; // время жизни снаряда (в секундах)
    public int damage = 1; // урон, который наносит снаряд
    public Vector2 target;
    public GameObject AnimationExplosion;
    private float birthTime; // время создания снаряда


    private void Start()
    {
        birthTime = Time.time; // запоминаем время создания снаряда
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
