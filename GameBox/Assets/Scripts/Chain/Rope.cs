using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D startObject;  // Начальный объект
    public Rigidbody2D endObject;    // Конечный объект
    public float ropeLength = 10f;  // Длина веревки
    public float ropeWidth = 0.1f;   // Ширина веревки

    private LineRenderer lineRenderer;

    void Start()
    {
        // Добавляем компонент DistanceJoint2D к начальному объекту
        DistanceJoint2D joint = startObject.gameObject.AddComponent<DistanceJoint2D>();

        // Устанавливаем свойства DistanceJoint2D
        joint.connectedBody = endObject;
        joint.distance = ropeLength;
        joint.maxDistanceOnly = true;
        joint.autoConfigureDistance = false;
    }
}
