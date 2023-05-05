using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private LayerMask _playerLayer;
    private LayerMask _ballLayer;
    private LayerMask _enemyLayer;
    private LayerMask _wallsLayer;
    private float _damage = 1f;
    private Rigidbody2D _rb;
    private Vector3 _velocity;
    private Owner _owner;
    private enum Owner
    {
        Enemy,
        Player
    }

    public float SetDamage
    {
        set { _damage = value; }
    }

    public Vector3 SetVelocity
    {
        set { _velocity = value; }
    }

    void Awake()
    {
        Destroy(gameObject, 7f);
    }

    private void Start()
    {
        _ballLayer = LayerMask.NameToLayer("Ball");
        _playerLayer = LayerMask.NameToLayer("Player");
        _enemyLayer = LayerMask.NameToLayer("Enemies");
        _wallsLayer = LayerMask.NameToLayer("Walls");
        _rb = transform.GetComponent<Rigidbody2D>();
        _rb.velocity = _velocity;
        _owner = Owner.Enemy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == _wallsLayer)
        {
            Debug.Log("Пуля умерла");
            Destroy(gameObject);
        }

        if (_owner == Owner.Enemy && collision.transform.gameObject.layer == _ballLayer)
        {
            Debug.Log("Пуля попала в шар");
            Debug.Log(_owner);
            _owner = Owner.Player;
            _rb.velocity = -_velocity;
            _velocity = -_velocity;
            transform.GetComponent<CapsuleCollider2D>().callbackLayers |= (1 << _enemyLayer);
            return;
        }

        if (_owner == Owner.Enemy && collision.transform.gameObject.layer == _playerLayer)
        {
            Debug.Log("Вражеская пуля попала в нас");
            collision.gameObject.GetComponent<Character>()?.GetDamage(_damage);
            Destroy(gameObject);
        }

        if (_owner == Owner.Player && collision.transform.gameObject.layer == _enemyLayer)
        {
            Debug.Log("Наша пуля попала во врага");
            collision.gameObject.GetComponent<BaseEnemy>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == _wallsLayer)
        {
            Debug.Log("Пуля умерла");
            Destroy(gameObject);
        }

        if (_owner == Owner.Enemy && collision.transform.gameObject.layer == _ballLayer)
        {
            Debug.Log("Пуля попала в шар");
            Debug.Log(_owner);
            _owner = Owner.Player;
            _rb.velocity = -_velocity;
            _velocity = -_velocity;
            transform.GetComponent<CapsuleCollider2D>().callbackLayers |= (1 << _enemyLayer);
            return;
        }

        if (_owner == Owner.Enemy && collision.transform.gameObject.layer == _playerLayer)
        {
            Debug.Log("Вражеская пуля попала в нас");
            collision.gameObject.GetComponent<Character>()?.GetDamage(_damage);
            Destroy(gameObject);
        }

        if (_owner == Owner.Player && collision.transform.gameObject.layer == _enemyLayer)
        {
            Debug.Log("Наша пуля попала во врага");
            collision.gameObject.GetComponent<BaseEnemy>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
