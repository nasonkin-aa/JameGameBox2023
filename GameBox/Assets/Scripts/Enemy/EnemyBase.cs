using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyBase : MonoBehaviour
{
    [SerializeReference]
    protected Transform _target;

    [SerializeField]
    protected float _speed = 5f;

    [SerializeField]
    protected PolygonCollider2D _attackCollider;

    [SerializeField]
    protected float _attackkDistance = 1f;

    [SerializeField]
    protected float _rotationSpeed = 180f;

    [SerializeField]
    protected float _hp = 1;

    [SerializeField]
    protected float _damage = 10;

    protected Rigidbody2D _rb;
    protected Collider2D _targetCollider;
    protected States _state = States.Moving;
    protected NavMeshAgent agent;
    protected Character _characterScript;

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
        _previousePos = transform.position;
        _characterScript = 
            _target.GetComponent<Character>() != null ? 
            _target.GetComponent<Character>() : 
            _target.gameObject.AddComponent<Character>();
    }


    protected virtual void Update()
    {
        if (_state == States.Moving)
            LookAtTargetInMoovment(_rotationSpeed);
    }

    protected virtual void FixedUpdate()
    {
        StateMachine();
    }

    protected virtual void MoveToTarget()
    {
        agent.SetDestination(_target.position);
    }

    protected virtual void LookAtTargetInMoovment(float rotationSpeed)
    {
        Vector2 moveDerection = (Vector2)transform.position - _previousePos;

        float angle = Mathf.Atan2(moveDerection.y, moveDerection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        _previousePos = transform.position;
    }

    protected virtual void LookAtTarget(float rotationSpeed)
    {
        Vector2 targetDerection = _target.position - transform.position;

        float angle = Mathf.Atan2(targetDerection.y, targetDerection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        _previousePos = transform.position;
    }


    protected virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(1); // под анмации

        if (_attackCollider.IsTouching(_targetCollider))
        {
            //Debug.Log("Персонажа задело");
            DealDamage();
        }
        //Debug.Log("Атака закончилась");
        agent.speed = 3;
        _state = States.Moving;
    }

    protected virtual void StateMachine()
    {        
        if (_state == States.Moving)
            MoveToTarget();

        if (_state != States.Attacking && DetectTarget()  )
        {
            //Debug.Log("Пошла атака");
            LookAtTarget(_rotationSpeed * 100); // костыль, резкий поворот
            _state = States.Attacking;
            _rb.velocity = Vector3.zero;
            agent.speed = 0;
            StartCoroutine(Attack());

        }
    }

    protected virtual IEnumerator Die()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        StopCoroutine(Attack()); /// нАдо ли, а вдруг пригодится
        _state = States.Dead;

        yield return new WaitForSeconds(1); // под анмации      
    }

    protected virtual bool DetectTarget ()
    {
        List<RaycastHit2D> resultsOfHit = new List<RaycastHit2D>();
        Vector2 targetDirection = (_target.position - transform.position).normalized;
        int numberOfHits = Physics2D.Raycast(transform.position, targetDirection, new ContactFilter2D().NoFilter(), resultsOfHit, _attackkDistance);
        Debug.DrawLine(transform.position, (Vector2)transform.position + targetDirection * _attackkDistance, Color.red);

        

        // Проверяем, попал ли луч в объект
        if (resultsOfHit.Any(hit => hit.transform == _target && !resultsOfHit.Any(hit => hit.transform.tag == "Wall"))) // поменять условие
        {
            //Debug.Log(numberOfHits);
            //Debug.Log("персонаж замечен");
            //Debug.Log(_state);
            return true;
        }
        return false;
    }

    protected virtual void DealDamage()
    {
        _characterScript.GetDamage(_damage);
    }
}