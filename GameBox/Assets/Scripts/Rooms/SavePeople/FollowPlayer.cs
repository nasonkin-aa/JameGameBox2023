using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target = null;
    public float followSpeed = 5f;
    public float stoppingDistance = 1f;
    public NavMeshAgent agent;
    private bool isFollowing = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SaveZoneFollowers>())
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            target = other.gameObject;
            isFollowing = true;
        }
        if(other.GetComponent<Character>())
        {
            target = other.gameObject;
            isFollowing = true;
        }
    }
    
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
  

    private void Update()
    {
        if (isFollowing && target != null)
        {
            agent.SetDestination(target.transform.position);
           
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
  
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance > stoppingDistance)
            {
                agent.speed = 3;          
            }
            else
            {
                agent.speed = 0;

            }
        }
    }
}
