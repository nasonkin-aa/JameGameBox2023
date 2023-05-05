using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public float radius = 5f; // радиус, в котором турель может обнаруживать героя
    public float fireRate = 1f; // скорость стрельбы турели (выстрелов в секунду)
    public GameObject bulletPrefab; // префаб снаряда
    public Transform bulletSpawnPoint; // точка, откуда будут появляться снаряды
    public GameObject Warning;

    private Transform heroTransform; // ссылка на героя
    private float nextFireTime; // время, когда турель сможет произвести следующий выстрел

    private void Start()
    {
        heroTransform = GameObject.FindObjectOfType<Character>().transform; // ищем героя в сцене по тегу
    }

    private void Update()
    {
        if (heroTransform == null)
        {
            return; // герой не найден - выходим из метода
        }

        // вычисляем расстояние между турелью и героем
        float distance = Vector2.Distance(transform.position, heroTransform.position);

        if (distance <= radius && Time.time >= nextFireTime)
        {
            // нацеливаем турель на героя
            bulletSpawnPoint.up = heroTransform.position - transform.position;

            // создаем новый снаряд
            GameObject gameObject =  Instantiate(Warning, heroTransform.position,Quaternion.identity);
            Destroy(gameObject,2f);
            StartCoroutine(ShootAfterDelay(heroTransform.position));
            //Shoot(heroTransform.position);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private IEnumerator ShootAfterDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(1.5f); // задержка в две секунды
        Shoot(targetPosition);
    }

    protected virtual void Shoot(Vector3 distance )
    {
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.GetComponent<BulletBehavior>().target = distance;
    }
}
