using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovingEnemy : BaseEnemy
{
    [SerializeField]
    protected float _speed = 5f;

    [SerializeField]
    protected float _rotationSpeed = 180f;

    protected NavMeshAgent agent;
    protected Vector2 _previousePos;
    protected int _layerMask;

    public override void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0 && _state != States.Dead)
            StartCoroutine(Die());
    }
    protected override void Start()
    {
        base.Start();
        _layerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Walls"));
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _previousePos = transform.position;
    }


    protected override void Update()
    {

    }

    protected override void FixedUpdate()
    {
        if (_state == States.Inactive)
            return;

        if (_state != States.Dead)
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
        
        if (_state == States.Dead)
            yield break;

        if (_attackCollider.IsTouching(_targetCollider))
        {
            DealDamage();
        }

        if (_state == States.Dead)
            yield break;
        _state = States.Moving;
    }

    protected virtual void StateMachine()
    {
        if (_state == States.Attacking || _state == States.Dead)
            return;

        if (DetectTarget())
        {
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
                return;
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
        _state = States.Dead;
        agent.speed = 0;
        //StopCoroutine(Attack()); /// нјдо ли, а вдруг пригодитс€
        

        yield return new WaitForSeconds(1); // под анмации      
    }

    protected virtual bool DetectTarget()
    {
        Vector2 targetDirection = TakeDirection(transform.position, _target.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, _attackkDistance, _layerMask);
        Debug.DrawLine(transform.position, (Vector2)transform.position + targetDirection * _attackkDistance, Color.red);

        if (hit.transform == _target.transform)
        {
            return true;
        }
        return false;
    }

    protected Vector2 TakeDirection (Vector2 point1, Vector2 point2)
    {
        return (point2 - point1).normalized;
    }

    protected Quaternion TakeRotationTo (Vector2 from, Vector2 to)
    {
        Vector2 targetDirection = TakeDirection(from, to);

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }

}

