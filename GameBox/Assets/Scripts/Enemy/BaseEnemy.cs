using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected enum States // ”далить лишнее и расширить в дочернем классе
    {
        Inactive,
        Attacking,
        Moving,
        Turns,
        Dead
    }
    [SerializeReference]
    protected Transform _target;

    [SerializeField]
    protected PolygonCollider2D _attackCollider;

    [SerializeField]
    protected float _attackkDistance = 1f;

    [SerializeField]
    protected float _hp = 1f;

    [SerializeField]
    protected float _damage = 10f;

    [SerializeField]
    protected States _state = States.Moving;

    [SerializeField]
    protected Aggression _aggressionEvent;


    protected Rigidbody2D _rb;
    protected Collider2D _targetCollider;
    protected Character _characterScript;
    public virtual void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0 && _state != States.Dead)
            StartCoroutine(Die());
    }

    protected virtual void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
        _targetCollider = _target.GetComponent<Collider2D>();
        _characterScript =
            _target.GetComponent<Character>() != null ?
            _target.GetComponent<Character>() :
            _target.gameObject.AddComponent<Character>();

        if (_state == States.Inactive)
        {
            _aggressionEvent = GetComponentInChildren<Aggression>();
            _aggressionEvent.OnAggressionEnter.AddListener(OnAggressionEnter);
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        if (_state == States.Inactive)
            return;
    }

    protected virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(1); // под анмации

        if (_attackCollider.IsTouching(_targetCollider))
        {
            DealDamage();
        }
    }
    protected virtual IEnumerator Die()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        StopCoroutine(Attack()); /// нјдо ли, а вдруг пригодитс€

        yield return new WaitForSeconds(1); // под анмации      
    }

    protected virtual void DealDamage()
    {
        _characterScript.GetDamage(_damage);
    }

    public void OnAggressionEnter(Transform target)
    {
        _state = States.Moving;
        _target = target;
        _aggressionEvent.OnAggressionEnter.RemoveListener(OnAggressionEnter);
    }
}
