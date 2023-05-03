using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeReference]
    protected Transform _target;
    [SerializeField]
    protected int _speed = 5;
    [SerializeField]
    protected Collider2D _attackCollider;
    [SerializeField]
    protected int _rotationSpeed = 5;

    protected Rigidbody2D _rb;
    protected Collider2D _targetCollider;
    protected States _state = States.Moving;

    protected enum States
    {
        Attacking,
        Moving,
        BeginsAttacking
    }

    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
        _targetCollider = _target.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_state == States.Moving)
            LookAtTarget();
    }

    private void FixedUpdate()
    {
        StateMachine();
    }

    protected virtual void MoveToTarget()
    {
        Vector3 directionToTarget = (_target.position - transform.position).normalized;

        _rb.velocity = directionToTarget * _speed;
    }

    protected virtual void LookAtTarget()
    {
        //ƒобавить плавность поворота после атаки
        gameObject.transform.LookAt(transform.position);
        transform.gameObject.SetActive(true);
        Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, 0);
        float angle = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 3.5f);
    }


    protected virtual IEnumerator Attack()
    {
        if (_state == States.Attacking)
        {
            yield break;
        }
        Debug.Log("Attack");
        _state = States.Attacking;

        //animator.SetTrigger("Attack");
        Debug.Log("Start Attack Animation");

        yield return new WaitForSeconds(1);

        //if (target != null)
        //{
        //    target.GetComponent<Health>().TakeDamage(damage);
        //}

        _state = States.Moving;
    }

    protected virtual void StateMachine()
    {
        Debug.Log(_state);
        if (_attackCollider.IsTouching(_targetCollider))
        {
            //Debug.Log("2");
            //_state = States.Attacking;
            _rb.velocity = Vector3.zero;
            StartCoroutine(Attack());
            //_state = States.Moving;
        }

        //Debug.Log("3");
        if (_state == States.Moving)
        {
            //Debug.Log("4");
            MoveToTarget();
        }
        //Debug.Log("5");


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("6");
    }

}
