using UnityEngine;
using UnityEngine.Events;

public class Aggression : MonoBehaviour
{
    public UnityEvent OnAggressionEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�������� ����� � ���");
        if (collision.gameObject.GetComponent<Character>())
            OnAggressionEnter.Invoke();
    }
}
