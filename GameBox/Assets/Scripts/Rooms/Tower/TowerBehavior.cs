using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public float radius = 5f; // ������, � ������� ������ ����� ������������ �����
    public float fireRate = 1f; // �������� �������� ������ (��������� � �������)
    public GameObject bulletPrefab; // ������ �������
    public Transform bulletSpawnPoint; // �����, ������ ����� ���������� �������

    private Transform heroTransform; // ������ �� �����
    private float nextFireTime; // �����, ����� ������ ������ ���������� ��������� �������
    private void Start()
    {
        heroTransform = GameObject.FindObjectOfType<Character>().transform; // ���� ����� � ����� �� ����
    }

    private void Update()
    {
        if (heroTransform == null)
        {
            return; // ����� �� ������ - ������� �� ������
        }

        // ��������� ���������� ����� ������� � ������
        float distance = Vector2.Distance(transform.position, heroTransform.position);

        if (distance <= radius && Time.time >= nextFireTime)
        {
            // ���������� ������ �� �����
            bulletSpawnPoint.up = heroTransform.position - transform.position;

            // ������� ����� ������
            Shoot(heroTransform.position);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    protected virtual void Shoot(Vector3 distance )
    {
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.GetComponent<BulletBehavior>().target = distance;
    }
}
