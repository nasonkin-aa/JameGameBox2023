using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DroneController : MonoBehaviour
{
    public float droneSpeed = 2f;

    public Vector3 targetPosition = new Vector3(0, 0, 0);
    private bool _carryingPlayer = false;
    private Transform playerTransform;
    private bool _reachedTarget = false;
    private float playerSpeed;
    private CapsuleCollider2D PlayerCollider2D;
    private CircleCollider2D BallCollider2D;


    private void FixedUpdate()
    {
        transform.Rotate(0f,0f, 500f* Time.deltaTime);
    }

    private void Start()
    {
        playerTransform = FindObjectOfType<Character>().GetComponent<Transform>();
        playerSpeed = playerTransform.GetComponent<Character>()._defaultSpeed;
        
        BallCollider2D = FindObjectOfType<ChainBall>().GetComponent<CircleCollider2D>();
        PlayerCollider2D = playerTransform.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!_reachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, droneSpeed * Time.deltaTime);

            var distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (!(distanceToPlayer < 0.5f)) return;
            _carryingPlayer = true;
            _reachedTarget = true;
        }
        else if (_carryingPlayer)
        {
            playerTransform.GetComponent<Character>()._defaultSpeed = 0;
            //playerTransform.parent.position = Vector3.MoveTowards(playerTransform.parent.position, targetPosition, droneSpeed * Time.deltaTime);
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, droneSpeed * Time.deltaTime);
            transform.position = playerTransform.position;
            
            PlayerCollider2D.enabled = false;
            BallCollider2D.enabled = false;
            
            var distanceToTarget = Vector3.Distance(playerTransform.position, targetPosition);
            if (distanceToTarget != 0f) return;
            
            playerTransform.GetComponent<Character>()._defaultSpeed = playerSpeed;
            
            
            _carryingPlayer = false;
            Destroy(gameObject, 2);
            
        }
        else
        {
            transform.position += Vector3.up * (droneSpeed * Time.deltaTime);
            PlayerCollider2D.enabled = true;
            BallCollider2D.enabled = true;
        }
    }
}