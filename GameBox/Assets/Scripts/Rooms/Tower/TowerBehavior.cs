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
            transform.GetChild(0).up = heroTransform.position - transform.position;

            // ������� ����� ������
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // ������ ����������� �������� �������
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bullet.GetComponent<BulletBehavior>().speed;

            // ������������� ����� ���������� ��������
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
