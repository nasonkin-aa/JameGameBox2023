using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TrapAroundObj : MonoBehaviour
{
  
    public GameObject spawnObject; // объект для спавна
    public int spawnCount = 100; // количество объектов для спавна
    public float spawnDistance = 1f; // расстояние от центра объекта до точек спавна
    public GameObject Worning;
    private Collider2D coll; // коллайдер границ объекта
    
    void Start()
    {
        // Получаем коллайдер границ объекта
        coll = GetComponent<Collider2D>();

        // Спавним объекты
        SpawnObjects();
    }

    void SpawnObjects()
    {
        List<GameObject> spearList = new List<GameObject>();
        // Цикл для спавна указанного количества объектов
        for (int i = 0; i < spawnCount; i++)
        {
            // Вычисляем случайный угол в радианах
            float angle = Random.Range(0f, Mathf.PI * 2f);


            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                Vector2 spawnPosition = (Vector2)transform.position + direction * spawnDistance;

                // Создаем новый объект
                float angle2 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle2 - 90));
                GameObject newObject = Instantiate(spawnObject, spawnPosition, targetRotation, transform);
                Destroy( newObject,4f );
                spearList.Add( newObject );
        }
                StartCoroutine(ObjectAnimation(spearList));
    }
    private IEnumerator ObjectAnimation(List<GameObject> list)
    {

        Worning.SetActive(true);
        yield return new WaitForSeconds(2);
        Worning.SetActive(false);
        float time = 0f;
        while (time < 2.2f)
        {
            foreach (GameObject spear in list)
            {
                time += Time.deltaTime ;
                Vector2 vector2 = new Vector2(Mathf.Cos(spear.transform.rotation.z * Mathf.Deg2Rad),
                    Mathf.Sin(spear.transform.rotation.z * Mathf.Deg2Rad)).normalized;

                spear.transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + (Vector2)spear.transform.up * 3, time);
            }
            yield return null;
        }
        yield return new WaitForSeconds(1);
         time = 0f;

        while (time < 2f)
        {
            foreach (GameObject spear in list)
            {
                time += Time.deltaTime;
                Vector2 vector2 = new Vector2(Mathf.Cos(spear.transform.rotation.z * Mathf.Deg2Rad),
                    Mathf.Sin(spear.transform.rotation.z * Mathf.Deg2Rad)).normalized;
                spear.transform.position = Vector2.MoveTowards(spear.transform.position, (Vector2)transform.position , time);
            }
            yield return null;
        }

        list.Clear();
        yield return new WaitForSeconds(3);
        SpawnObjects();
    }

}
