using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public float radius = 5f; // радиус, в котором турель может обнаруживать героя
    public float fireRate = 1f; // скорость стрельбы турели (выстрелов в секунду)
    public GameObject bulletPrefab; // префаб снаряда
    public Transform bulletSpawnPoint; // точка, откуда будут появляться снаряды

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

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // задаем направление движения снаряда
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bullet.GetComponent<BulletBehavior>().speed;

            // устанавливаем время следующего выстрела
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
