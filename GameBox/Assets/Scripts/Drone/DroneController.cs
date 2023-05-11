using UnityEngine;
using UnityEngine.Events;

public class DroneController : MonoBehaviour
{
    public float droneSpeed = 2f;

    public Vector3 targetPosition = new Vector3(0, 0, 0);
    private bool _carryingPlayer = false;
    private Transform playerTransform;
    private bool _reachedTarget = false;
    //private float playerSpeed;
    private CapsuleCollider2D PlayerCollider2D;
    private CircleCollider2D BallCollider2D;
    public UnityEvent OnPlayereTaken;
    private Character _char => playerTransform.GetComponent<Character>();


    private void FixedUpdate()
    {
        transform.Rotate(0f,0f, 500f* Time.deltaTime);
    }

    private void Start()
    {
        playerTransform = FindObjectOfType<Character>().GetComponent<Transform>();
        
        BallCollider2D = FindObjectOfType<ChainBall>().GetComponent<CircleCollider2D>();
        PlayerCollider2D = playerTransform.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!_reachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, droneSpeed * Time.deltaTime);

            var distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (!(distanceToPlayer < 0.5f)) 
                return;

            _carryingPlayer = true;
            OnPlayereTaken.Invoke();
            _reachedTarget = true;
        }
        else if (_carryingPlayer)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, droneSpeed * Time.deltaTime);
            transform.position = playerTransform.position;

            _char.Disable();
            BallCollider2D.enabled = false;
            
            var distanceToTarget = Vector3.Distance(playerTransform.position, targetPosition);
            if (distanceToTarget != 0f) 
                return;

            _char.Enable();        
            
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