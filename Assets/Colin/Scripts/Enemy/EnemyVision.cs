using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyMovement;

    LayerMask wallMask;
    LayerMask layerMask;
    LayerMask playerMask;
    GameObject player;
    public float visionRange = 10;
    public static event Action<bool> enemySees;
    [Range(1,360)] public float detectionAngle = 45;
    private PlayerController playerController;
    public float meshRelosution;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    void Awake()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        player = GameObject.Find("Player");
        playerMask = LayerMask.GetMask("Player");
        layerMask = LayerMask.GetMask("Player", "Wall");
        wallMask = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (CheckInAngle() && CheckIsNotHidden())
        {
            if(!this.gameObject.CompareTag("Dark Elf") && CheckPlayerInShadow())
            {
                enemyMovement.seePlayer = false;
                return;
            }
            ChangeAngle();
            enemyMovement.sawPlayer = true;
            enemyMovement.seePlayer = true;
            enemySees?.Invoke(true);
            Debug.Log("Caught");
        }
        else
        {
            enemyMovement.seePlayer = false;
        }
        Debug.Log(CheckInAngle());
        Debug.Log(CheckIsNotHidden());
       // DrawFOV();
    }

    bool CheckInAngle()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, visionRange, playerMask);
        if (rangeCheck != null)
        {
            Debug.Log("Yes");
            Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
            if (rangeCheck.transform.CompareTag("Player"))
            {
                Debug.Log("Yes");
                if (Vector3.Angle(transform.up, directionToTarget) < detectionAngle * 0.5f)
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

    /*void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(detectionAngle * meshRelosution);
        float stepAngleSize = detectionAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = - detectionAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangle = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangle[i * 3] = 0;
                triangle[i * 3 + 1] = i + 1;
                triangle[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangle;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(-transform.eulerAngles.z, globalAngle);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionRange, wallMask);

        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            Debug.Log("Debug");
            return new ViewCastInfo(false, transform.position + direction * visionRange, visionRange, globalAngle);
        }
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, visionRange);

        Vector3 angleOne = DirectionFromAngle(-transform.eulerAngles.z, -detectionAngle * 0.5f);
        Vector3 angleTwo = DirectionFromAngle(-transform.eulerAngles.z, detectionAngle * 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + angleOne * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + angleTwo * visionRange);
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void ChangeAngle()
    {
        Transform target = player.transform;

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, (target.position - transform.position));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (enemyMovement.rotationSpeed + 2)  * Time.deltaTime);
    }

    /*public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo (bool hit, Vector3 point, float distance, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.distance = distance;
            this.angle = angle;
        }
    }
    */
}
