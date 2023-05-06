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

        // Добавляем компонент LineRenderer к начальному объекту
        lineRenderer = startObject.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
    }

    void Update()
    {
        // Обновляем точки отрисовки для LineRenderer
        lineRenderer.SetPosition(0, startObject.transform.position);
        lineRenderer.SetPosition(1, endObject.transform.position);
    }
}
