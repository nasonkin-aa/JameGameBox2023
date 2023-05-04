using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeReference]
    protected Transform _target;

    [SerializeField]
    protected float _speed = 5f;

    [SerializeField]
    protected PolygonCollider2D _attackCollider;

    protected float _rotationSpeed = 180f;

    protected Rigidbody2D _rb;
    protected Collider2D _targetCollider;
    protected float _hp = 1;
    protected States _state = States.Moving;
    protected NavMeshAgent agent;

    protected Vector2 _previousePos;
    protected enum States
    {
        Attacking,
        Moving,
        Dead
    }

    public virtual void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0 && _state != States.Dead)
            StartCoroutine(Die());
    }

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _rb = transform.GetComponent<Rigidbody2D>();
        _targetCollider = _target.GetComponent<Collider2D>();
        _previousePos = (Vector2)transform.position;
    }


    protected virtual void Update()
    {
        if (_state == States.Moving)
            LookAtTarget();
    }

    protected virtual void FixedUpdate()
    {
        StateMachine();
    }

    protected virtual void MoveToTarget()
    {
        /*  
                Vector3 directionToTarget = (_target.position - transform.position).normalized;
                _rb.velocity = directionToTarget * _speed;*/
        agent.SetDestination(_target.position);
    }

    protected virtual void LookAtTarget()
    {
        
        Vector2 moveDerection = (Vector2)transform.position - _previousePos;

        float angle = Mathf.Atan2(moveDerection.y, moveDerection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _previousePos = transform.position;
    }


    protected virtual IEnumerator Attack()
    {
        if (_state == States.Attacking)
        {
            yield break;
        }

        _state = States.Attacking;
        agent.enabled = false;
        yield return new WaitForSeconds(1); // под анмации
        agent.enabled = true;

        _state = States.Moving;
    }

    protected virtual void StateMachine()
    {
        if (_attackCollider.IsTouching(_targetCollider))
        {
            _rb.velocity = Vector3.zero;
            StartCoroutine(Attack());
        }

        if (_state == States.Moving)
        {
            MoveToTarget();
        }
    }

    protected virtual IEnumerator Die()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        StopCoroutine(Attack()); /// нјдо ли, а вдруг пригодитс€
        _state = States.Dead;

        yield return new WaitForSeconds(1); // под анмации      
    }
}