using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject eyes;
    private GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprite;

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
    public bool canMove;
    private int lookDirection = 1;
    private Quaternion lookRotation;
    public bool loop;
    private Vector3 startPostition;
    private Quaternion originalRotation;
    private float switchTimer = 1;
    

    [HideInInspector] public bool sawPlayer;
    [HideInInspector] public bool seePlayer;

    void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        canMove = true;
        player = GameObject.Find("Player");

        startPostition = transform.position;
        originalRotation = eyes.transform.rotation;
    }

    private void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (canMove && !stunned)
        {
            if (!sawPlayer)
            {
                Patrol(); 
                ChangeAnimator();
            }
            else if (sawPlayer && seePlayer)
            {
                ChasePlayer();
                ChangeAnimator();
                lookTime = 0;
            }
            else if (sawPlayer && !seePlayer)
            {
                if (AtLastKnown())
                {
                    GoToLastKnown();
                    ChangeAnimator();
                    lookRotation = transform.rotation * Quaternion.Euler(0, 0, 30);
                }
                else
                {
                    LookForPlayer();
                    ChangeAnimator();
                }
            }
        }
    }

    void Patrol()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }
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

        if (waypoints.Length == 0 && agent.transform.position == startPostition && eyes.transform.rotation != originalRotation)
        {
            eyes.transform.rotation = originalRotation;
        }

        if ((transform.position - target.position).sqrMagnitude < reachDistance * reachDistance)
        {
            if (!loop && waypoints.Length > 1)
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
            }
            else
            {
                currentWaypointTarget = (currentWaypointTarget + 1) % waypoints.Length;
            }
            waitTime = Random.Range(1f, 3f);
        }
        else
        {
            agent.SetDestination(target.position);
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
            if (waypoints.Length == 0)
            {
                agent.SetDestination(startPostition);
            }
            sawPlayer = false;
        }
    }

    void ChasePlayer()
    {
        Transform target = player.transform;

        agent.SetDestination(target.position);
        lastKnown = target.position;
    }

    void GoToLastKnown()
    {
        Vector3 target = lastKnown;
        
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, (target - transform.position));
        eyes.transform.rotation = Quaternion.Slerp(eyes.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        agent.SetDestination(target);
    }

    bool AtLastKnown()
    {
            return agent.hasPath;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
            Invoke("CanMove", 2);
        }
    }

    private void CanMove()
    {
        canMove = true;
    }

    private void ChangeAnimator()
    {
        if (!gameObject.CompareTag("Slime"))
        {
            if (agent.velocity.x > 0.3 || agent.velocity.x < -0.3)
            {
                spriteRenderer.sprite = sprite[1];
                if (agent.velocity.x > 0)
                {
                    spriteRenderer.flipX = false;
                    return;
                }
                spriteRenderer.flipX = true;
            }
            else if (agent.velocity.y > 0.001)
            {
                spriteRenderer.sprite = sprite[2];
            }
            else if (agent.velocity.y < -0.001)
            {
                spriteRenderer.sprite = sprite[0];
            }
        }
        else
        {
            switchTimer -= Time.deltaTime;
            if (switchTimer <= 0 && spriteRenderer == sprite[0])
            {
                spriteRenderer.sprite = sprite[1];
                switchTimer = 1;
            }
            else if (switchTimer <= 0 && spriteRenderer == sprite[1])
            {
                spriteRenderer.sprite = sprite[0];
                switchTimer = 1;
            }
        }
    }
}
