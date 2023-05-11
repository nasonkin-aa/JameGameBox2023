using UnityEngine;

public class Spiner : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0f,0f, -500f* Time.deltaTime);
    }
}
