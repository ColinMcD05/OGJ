using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    public int currentWayPointTarget = 0;

    public float moveSpeed = 4;
    public float waitTime = 3;

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

        Transform target = waypoints[currentWayPointTarget];
    }

    void LookForPlayer()
    {

    }

    void ChasePlayer()
    {

    }
}
