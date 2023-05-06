using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected enum States // ������� ������ � ��������� � �������� ������
    {
        Inactive,
        Attacking,
        Moving,
        Turns,
        Dead
    }
    protected Transform _target;

    [SerializeField]
    protected PolygonCollider2D _attackCollider;

    [SerializeField]
    protected float _attackkDistance = 1f;

    [SerializeField]
    protected float _hp = 1;

    [SerializeField]
    protected float _damage = 10;

    [SerializeField]
    protected States _state = States.Attacking;

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
        _target = Character.TargetGameObject.transform;
        
        _rb = transform.GetComponent<Rigidbody2D>();
        _targetCollider = _target.GetComponent<Collider2D>();
        _characterScript =
            _target.GetComponent<Character>() != null ?
            _target.GetComponent<Character>() :
            _target.gameObject.AddComponent<Character>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(1); // ��� �������

        if (_attackCollider.IsTouching(_targetCollider))
        {
            DealDamage();
        }
    }
    protected virtual IEnumerator Die()
    {
        Debug.Log("�������");
        gameObject.GetComponent<Collider2D>().enabled = false;
        StopCoroutine(Attack()); /// ���� ��, � ����� ����������

        yield return new WaitForSeconds(1); // ��� �������      
    }

    protected virtual void DealDamage()
    {
        _characterScript.GetDamage(_damage);
    }


}
