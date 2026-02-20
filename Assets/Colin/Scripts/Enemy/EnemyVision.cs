using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyMovement;

    LayerMask layerMask;
    GameObject player;
    public float visionRange = 10;
    [Range(1,360)] public float detectionAngle = 45;

    void Awake()
    {
        player = GameObject.Find("Player");
        layerMask = LayerMask.GetMask("Player", "Wall");
    }

    void Update()
    {
        if (CheckInAngle() && CheckIsNotHidden())
        {
            if(!this.gameObject.CompareTag("Dark Elf") && CheckPlayerInShadow())
            {
                enemyMovement.seePlayer = false;
                return;
            }
            enemyMovement.sawPlayer = true;
        }
        else
        {
            enemyMovement.seePlayer = false;
        }
        // Debug.Log(CheckIsNotHidden());
    }

    bool CheckInAngle()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, visionRange);
        if (rangeCheck != null)
        {
            Vector2 directionToTarget = (player.transform.position - transform.position).normalized;
            if (rangeCheck.transform.CompareTag("Player"))
            {
                if (Vector2.Angle(transform.up, directionToTarget) < detectionAngle)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool CheckIsNotHidden()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, (player.transform.position - transform.position), visionRange, layerMask);
        if (ray.collider != null)
        {
            return ray.collider.CompareTag("Player");
        }
        return false;
    }

    bool CheckPlayerInShadow()
    {
        return player.GetComponent<PlayerController>().inShadow;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, visionRange);

        Vector3 angleOne = DirectionFromAngle(-transform.eulerAngles.z, -detectionAngle / 2);
        Vector3 angleTwo = DirectionFromAngle(-transform.eulerAngles.z, detectionAngle / 2);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + angleOne * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + angleTwo * visionRange);
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
