using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 5f;
    public float stoppingDistance = 1f;
    public NavMeshAgent agent;
    private bool isFollowing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if( other.GetComponent<Character>())
            player = other.gameObject;
            isFollowing = true;
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
  

    private void Update()
    {
        if (isFollowing && player != null)
        {
            agent.SetDestination(player.transform.position);

            /*float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;
            Vector2 direction = player.transform.position - transform.position;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/
            float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            /*Quaternion targetRotation = TakeRotationTo(transform.position, player.transform.position);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 3 * Time.deltaTime);
*/
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance > stoppingDistance)
            {
                agent.speed = 3;
               // transform.position += (Vector3)direction.normalized * followSpeed * Time.deltaTime;
            }
            else
            {
                agent.speed = 0;

            }
        }
    }
}
