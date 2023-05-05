using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    protected float _hp = 1;

    private Vector3 mousePos;

    public float speedChar = 5f;

    public  Rigidbody2D rbChar;
    protected bool _isMovingBlock = false;

    public bool IsMovingBlock 
    {
        get { return _isMovingBlock; }
        set { _isMovingBlock = !_isMovingBlock; }
    }

    public virtual void GetDamage(float damage)
    {
        //Debug.Log("Получен урон");
        _hp -= damage;
        if (_hp <= 0)
            StartCoroutine(Die());
    }

    private void Start()
    {
        rbChar = GetComponent<Rigidbody2D>();
   
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
            rbChar.velocity = Vector2.zero;
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

        rbChar.velocity = movement * speedChar;
    }


    private void LookAt()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10; 
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected virtual IEnumerator Die()
    {
        //StopCoroutine(); /// нАдо ли, а вдруг пригодится
        Debug.Log("Персонаж умер, увы");

        yield return new WaitForSeconds(1); // под анмации      
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
}
