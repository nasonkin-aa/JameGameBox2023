using UnityEngine;

public class RangeEnemy : MovingEnemy
{
    [SerializeField]
    protected GameObject _projectileSpawnPoint;

    [SerializeReference]
    protected GameObject _projectile;

    [SerializeReference]
    protected bool _IsMovable = true;
    
    [SerializeReference]
    protected float _bulletSpeed = 100f; // ��������, ����� ��������� � ���� � ����� �� ����
    protected Transform _projectilePoint;

    protected override void Start()
    {
        
        base.Start();
        _projectilePoint = _projectileSpawnPoint.transform;
    }


    protected override void DealDamage()
    {
        Shoot(_target.position - _projectilePoint.position);
    }

    protected virtual void Shoot(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(_projectile, _projectilePoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));

        bullet.GetComponent<Projectile>().SetDamage = _damage;
        bullet.GetComponent<Projectile>().SetVelocity = direction.normalized * _bulletSpeed;
    }

    protected override void MoveToTarget()
    {
        if (!_IsMovable)
            return;

        agent.SetDestination(_target.position);
    }
}
