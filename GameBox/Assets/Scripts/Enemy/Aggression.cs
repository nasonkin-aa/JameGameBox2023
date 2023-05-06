using UnityEngine;
using UnityEngine.Events;

public class Aggression : MonoBehaviour
{
    public UnityEvent<Transform> OnAggressionEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() || collision.gameObject.GetComponent<FollowPlayer>())
            OnAggressionEnter.Invoke(collision.transform);
    }
}
