using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyBase
{
    [SerializeField]
    protected GameObject _projectileSpawnPoint;
    [SerializeReference]
    protected GameObject _projectile;

    protected float _bulletSpeed = 100f; // заглушка, можно перенести в пулю и взять от туда
    protected Transform _projectilePoint;

    protected override void Start()
    {
        base.Start();
        _projectilePoint = _projectileSpawnPoint.transform;
    }

    protected override void  Update()
    {
        base.Update();
    }

    protected override IEnumerator Attack()
    {
        if (_state == States.Attacking)
        {
            yield break;
        }

        _state = States.Attacking;
        Vector3 targetDirection = _target.position - _projectilePoint.position;

        yield return new WaitForSeconds(1);
        Shoot(targetDirection);
        yield return new WaitForSeconds(0.2f);

        _state = States.Moving;
    }

    protected virtual void Shoot(Vector3 direction)
    {
        Debug.Log("Shoot");

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(_projectile, _projectilePoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));

        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _bulletSpeed;
    }
}
