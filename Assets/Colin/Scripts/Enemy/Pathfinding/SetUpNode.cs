using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class SetUpNode : MonoBehaviour
{

    public Grid[] grid;
    private Tilemap tilemap;
    BoundsInt bounds;

    public Node nodePrefab;
    public List<Node> nodeList;

    public EnemyMovement enemyMovement;

    private bool canDrawGizmos;

    void Awake()
    {
        tilemap = GameObject.Find("Floor").GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
    }

    void CreateNodes()
    {

    }

    void CreateConnections()
    {

    }

    void ConnectNodes(Node from, Node to)
    {

    }
}
