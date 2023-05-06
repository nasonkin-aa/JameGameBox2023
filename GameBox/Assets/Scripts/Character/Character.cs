using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public static GameObject TargetGameObject;

    public UnityEvent OnDie;
    public UnityEvent OnTakeCoin;

    [SerializeField]
    protected float _hp = 1;

    [SerializeField]
    [Range(0f, 1f)]
    protected float _slow—oefficient = 0.3f;

    [SerializeField]
    [Range(3f, 20f)]
    protected float _defaultSpeed = 5f;

    private Vector3 _mousePos;
    protected float _speedChar;

    protected  Rigidbody2D _rbChar;
    protected bool _isMovingBlock = false;
    protected PickUpZone _pickUpZote;

    public bool IsMovingBlock 
    {
        get { return _isMovingBlock; }
        set { _isMovingBlock = !_isMovingBlock; }
    }

    public virtual void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
            StartCoroutine(Die());
    }

    private void Awake()
    {
        TargetGameObject = gameObject;
    }

    private void Start()
    {
        _rbChar = GetComponent<Rigidbody2D>();
        _pickUpZote = GetComponentInChildren<PickUpZone>();
        _pickUpZote.OnBallPickUp.AddListener(OnBallPickUped);
        _pickUpZote.OnBallDrop.AddListener(OnBallDropped);
        _speedChar = _defaultSpeed;
    }
    private void Update()
    {
        if (!_isMovingBlock)
            LookAt();

    }
    private void FixedUpdate()
    {
        if (!_isMovingBlock)
            PlayerControler();
        else
            _rbChar.velocity = Vector2.zero;
    }
    private void PlayerControler()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1f;
        }

        movement.Normalize();

        _rbChar.velocity = movement * _speedChar;
    }


    private void LookAt()
    {
        _mousePos = Input.mousePosition;
        _mousePos.z = 10; 
        _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
        float angle = Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected virtual IEnumerator Die()
    {
        OnDie.Invoke();

        yield return new WaitForSeconds(1); 
    }

    protected Vector2 TakeDirection(Vector2 point1, Vector2 point2)
    {
        return (point2 - point1).normalized;
    }

    protected Quaternion TakeRotationTo(Vector2 from, Vector2 to)
    {
        Vector2 targetDirection = TakeDirection(from, to);

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnBallPickUped()
    {
        _speedChar = _defaultSpeed * _slow—oefficient;
    }

    public void OnBallDropped()
    {
        _speedChar = _defaultSpeed;
    }

    public void TakeCoin()
    {
        OnTakeCoin.Invoke();
    }
}
