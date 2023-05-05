using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class MovingEnemy : BaseEnemy
{
    [SerializeField]
    protected float _speed = 5f;

    [SerializeField]
    protected float _rotationSpeed = 180f;

    protected NavMeshAgent agent;
    protected Vector2 _previousePos;


    public override void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0 && _state != States.Dead)
            StartCoroutine(Die());
    }
    protected override void Start()
    {
        base.Start();
        _state = States.Moving;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _previousePos = transform.position;
    }


    protected override void Update()
    {
        //if (_state == States.Moving)
        //    LookAtTarget(_rotationSpeed, _previousePos); // При движении
    }

    protected override void FixedUpdate()
    {
        StateMachine();
    }

    protected virtual void MoveToTarget()
    {
        agent.SetDestination(_target.position);
    }

    protected virtual void LookAtTarget(float rotationSpeed, Vector2 from, Vector2 to)
    {
        Quaternion targetRotation = TakeRotationTo(from, to);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        _previousePos = transform.position;
    }


    protected override IEnumerator Attack()
    {
        yield return new WaitForSeconds(1); // под анмации

        if (_attackCollider.IsTouching(_targetCollider))
        {
            DealDamage();
        }
        _state = States.Moving;
    }

    protected virtual void StateMachine()
    {
        //if (_state == States.Moving)
        //    MoveToTarget();

        //if (_state != States.Attacking && DetectTarget())
        //{
        //    LookAtTarget(_rotationSpeed * 100, transform.position, _target.position); // костыль, резкий поворот // Стоя на месте
        //    _state = States.Attacking;
        //    agent.speed = 0;
        //    StartCoroutine(Attack());
        //}

        if (_state == States.Attacking)
            return;

        if (DetectTarget())
        {
            Debug.Log(Quaternion.Angle(TakeRotationTo(transform.position, _target.position), transform.rotation));
            if (Mathf.Abs(Quaternion.Angle(TakeRotationTo(transform.position, _target.position), transform.rotation)) > 5)
            {
                
                _state = States.Turns;
                LookAtTarget(_rotationSpeed, transform.position, _target.position);
                return;
            } 
            else 
            {
                _state = States.Attacking;
                agent.speed = 0;
                StartCoroutine(Attack());
            }
        } 
        else 
        {
            _state = States.Moving;
            agent.speed = 3;
            LookAtTarget(_rotationSpeed, _previousePos, transform.position);
            MoveToTarget();
            return;
        }
    }

    protected override IEnumerator Die()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        StopCoroutine(Attack()); /// нАдо ли, а вдруг пригодится
        _state = States.Dead;

        yield return new WaitForSeconds(1); // под анмации      
    }

    protected virtual bool DetectTarget()
    {
        List<RaycastHit2D> resultsOfHit = new List<RaycastHit2D>();
        Vector2 targetDirection = (_target.position - transform.position).normalized;

        int numberOfHits = Physics2D.Raycast(transform.position, targetDirection, new ContactFilter2D().NoFilter(), resultsOfHit, _attackkDistance);
        Debug.DrawLine(transform.position, (Vector2)transform.position + targetDirection * _attackkDistance, Color.red);

        // Проверяем, попал ли луч в объект
        if (resultsOfHit.Any(hit => hit.transform == _target && !resultsOfHit.Any(hit => hit.transform.tag == "Wall")))
        {
            return true;
        }
        return false;
    }

    protected Vector2 TakeDirection (Vector2 point1, Vector2 point2)
    {
        return point2 - point1;
    }

    protected Quaternion TakeRotationTo (Vector2 from, Vector2 to)
    {
        Vector2 targetDerection = TakeDirection(to, from);

        float angle = Mathf.Atan2(targetDerection.y, targetDerection.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

