using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBehavior : MonoBehaviour
{
    public float radius = 5f; // ������, � ������� ������ ����� ������������ �����
    public float fireRate = 1f; // �������� �������� ������ (��������� � �������)
    public GameObject bulletPrefab; // ������ �������
    public Transform bulletSpawnPoint; // �����, ������ ����� ���������� �������
    public GameObject Warning;
    
    public UnityEvent OnFinishLevel;


    private Transform heroTransform; // ������ �� �����
    private float nextFireTime; // �����, ����� ������ ������ ���������� ��������� �������
    public int HpTower = 10;

    private void Start()
    {
        heroTransform = GameObject.FindObjectOfType<Character>().transform; // ���� ����� � ����� �� ����
    }

    public void Die()
    {
        OnFinishLevel.Invoke();
        Destroy(gameObject);
    }
    private void Update()
    {
        if (HpTower <= 0) 
        {
            Die();
        }

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
            GameObject gameObject =  Instantiate(Warning, heroTransform.position,Quaternion.identity);
            Destroy(gameObject,2f);
            StartCoroutine(ShootAfterDelay(heroTransform.position));
            //Shoot(heroTransform.position);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private IEnumerator ShootAfterDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(1.5f); // �������� � ��� �������
        Shoot(targetPosition);
    }

    protected virtual void Shoot(Vector3 distance )
    {
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.GetComponent<BulletBehavior>().target = distance;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.GetComponent<ChainBall>())
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 4)
            {
                HpTower--;
            }
        }
    }
}
