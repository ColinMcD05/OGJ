using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject eyes;
    private GameObject player;

    [SerializeField] Transform[] waypoints;
    public int currentWaypointTarget = 0;

    public float moveSpeed = 4;
    public float reachDistance = 0.1f;
    public float waitTime = 3;
    private int moveDirection = 1;

    [HideInInspector] public bool sawPlayer;
    [HideInInspector] public bool seePlayer;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

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

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, (target.position - transform.position));
        eyes.transform.rotation = targetRotation;

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
        Transform target = player.transform;
    }

    void ChasePlayer()
    {

    }
}
