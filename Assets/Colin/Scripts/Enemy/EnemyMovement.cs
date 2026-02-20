using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public int rotationSpeed = 5;
    private Vector3 lastKnown;
    private float lookTime = 0;
    private bool stunned;
    private bool canMove;
    private int lookDirection = 1;
    private Quaternion lookRotation;

    [HideInInspector] public bool sawPlayer;
    [HideInInspector] public bool seePlayer;

    void Awake()
    {
        canMove = true;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (canMove && !stunned)
        {
            if (!sawPlayer)
            {
                Patrol();
            }
            else if (sawPlayer && seePlayer)
            {
                ChasePlayer();
                lookTime = 0;
            }
            else if (sawPlayer && !seePlayer)
            {
                if (!AtLastKnown())
                {
                    GoToLastKnown();
                    lookRotation = transform.rotation * Quaternion.Euler(0, 0, 30);
                }
                else
                {
                    LookForPlayer();
                }
            }
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

        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, direction);
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
        lookTime += Time.deltaTime;
        if (lookTime >= 1 && lookTime <= 4.5f)
        {
            if (transform.rotation != lookRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (rotationSpeed-2) * Time.deltaTime);
            }
            else
            {
                lookDirection *= -1;
                lookRotation *= Quaternion.Euler(0,0,30 * lookDirection);
            }
        }
        if (lookTime >= 5)
        {
            sawPlayer = false;
        }
    }

    void ChasePlayer()
    {
        Transform target = player.transform;

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, (target.position - transform.position));
        eyes.transform.rotation = Quaternion.Slerp(eyes.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        lastKnown = target.position;
    }

    void GoToLastKnown()
    {
        Vector3 target = lastKnown;
        
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, (target - transform.position));
        eyes.transform.rotation = Quaternion.Slerp(eyes.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    bool AtLastKnown()
    {
        return transform.position == lastKnown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;
        }
    }
}
