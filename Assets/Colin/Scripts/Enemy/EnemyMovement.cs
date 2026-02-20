using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject eyes;

    [SerializeField] Transform[] waypoints;
    public int currentWaypointTarget = 0;

    public float moveSpeed = 4;
    public float reachDistance = 0.1f;
    public float waitTime = 3;
    private int moveDirection = 1;

    [HideInInspector] public bool sawPlayer;
    [HideInInspector] public bool seePlayer;

    private void Update()
    {
        if (!sawPlayer)
        {
            Patrol();
        }
        else if (sawPlayer && seePlayer)
        {
            ChasePlayer();
        }
        else if (sawPlayer && !seePlayer)
        {
            LookForPlayer();
        }
    }

    void Patrol()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        Transform target = waypoints[currentWaypointTarget];

        Vector2 direction = (target.position - transform.position).normalized;
        if(direction != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            eyes.transform.rotation = targetRotation;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        if ((transform.position - target.position).sqrMagnitude < reachDistance * reachDistance)
        {
            if (currentWaypointTarget == waypoints.Length - 1)
            {
                moveDirection = -1;
            }
            else if (currentWaypointTarget == 0)
            {
                moveDirection = 1;
            }
            currentWaypointTarget = (currentWaypointTarget + 1 * moveDirection);
            waitTime = Random.Range(1f, 3f);
        }
    }

    void LookForPlayer()
    {

    }

    void ChasePlayer()
    {

    }
}
