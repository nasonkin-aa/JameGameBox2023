using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D startObject;  // ��������� ������
    public Rigidbody2D endObject;    // �������� ������
    public float ropeLength = 10f;  // ����� �������
    public float ropeWidth = 0.1f;   // ������ �������

    private LineRenderer lineRenderer;

    void Start()
    {
        // ��������� ��������� DistanceJoint2D � ���������� �������
        DistanceJoint2D joint = startObject.gameObject.AddComponent<DistanceJoint2D>();

        // ������������� �������� DistanceJoint2D
        joint.connectedBody = endObject;
        joint.distance = ropeLength;
        joint.maxDistanceOnly = true;
        joint.autoConfigureDistance = false;
    }
}
