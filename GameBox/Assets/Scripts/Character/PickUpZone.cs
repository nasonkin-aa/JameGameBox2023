using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickUpZone : MonoBehaviour
{
    private bool isBeingCarried = false; // ����, ����������� �� ��, ��� ������ ����
    private bool throwBlock = false;
    public GameObject Ball; 
    private Rigidbody2D rbBall; // ������ �� Rigidbody �������, ������� ����� ������������
    public float pushSpeed = 10f;
    public GameObject Char;
    private Character _characterScript;
    protected Coroutine threw;
    public UnityEvent OnBallPickUp;
    public UnityEvent OnBallDrop;

    void Start ()
    {
        rbBall = Ball.GetComponent<Rigidbody2D>();
        _characterScript = Char.GetComponent<Character>();
    }

    IEnumerator MoveCharacter(Vector2 targetPosition)
    {
        float time = 0f;
        Vector3 startPosition = Char.transform.position;
        yield return new WaitForSeconds(0.2f);
        while (time < 1f)
        {
            time += Time.deltaTime * 10 ;
            Char.transform.position = Vector3.Lerp(startPosition, Ball.transform.position, time);
            yield return new WaitForSeconds(0.01f);
        }
        _characterScript.IsMovingBlock = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && transform.GetComponent<Collider2D>().IsTouchingLayers())
        {
            OnBallPickUp.Invoke();
            isBeingCarried = true;
        }
        else
        {
            OnBallDrop.Invoke();
            isBeingCarried = false;
        }

        if (isBeingCarried)
        {
            if (!throwBlock) // ���� ������ ����
                Ball.transform.position = transform.position;
            if (Input.GetMouseButton(1) && !throwBlock)
            {
                OnBallDrop.Invoke();
                ThrowBall();
            }
        }
    }

    private void ThrowBall()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)Ball.transform.position).normalized;
        rbBall.velocity = direction * pushSpeed;
        StartCoroutine(ThrowBlocking());
        _characterScript.IsMovingBlock = true;
        StartCoroutine(MoveCharacter(Char.transform.position + (transform.position - Char.transform.position).normalized * 3));
    }

    private IEnumerator ThrowBlocking()
    {
        throwBlock = true;
        yield return new WaitForSeconds(0.9f);
        throwBlock = false;
    }
}
