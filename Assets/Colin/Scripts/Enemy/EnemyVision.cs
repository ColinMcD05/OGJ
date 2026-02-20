using UnityEngine;
using UnityEngine.UIElements;

public class EnemyVision : MonoBehaviour
{
    LayerMask layerMask;

    GameObject player;
    public float visionRange = 10;
    [Range(1,360)] public float detectionAngle = 45;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Player", "Walls");
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (CheckInAngle() && CheckIsNotHidden())
        {
            Debug.Log("You're Caught!");
        }
        Debug.Log(CheckIsNotHidden());
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
        RaycastHit2D ray = Physics2D.Raycast(transform.position, (player.transform.position - transform.position), visionRange);
        if (ray.collider != null)
        {
            return ray.collider.CompareTag("Player");
        }
        return false;
    }
}
