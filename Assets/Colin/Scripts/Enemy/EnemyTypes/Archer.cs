using UnityEngine;

public class Archer : MonoBehaviour
{
    GameObject player;
    [SerializeField] EnemyMovement enemyMovement;

    public int health;
    public int stunHealth;
    public float range = 5;
    public float cooldownTimer = 5;
    public float windUp = 2;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (enemyMovement.seePlayer && cooldownTimer <= 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > range)
        {
            enemyMovement.canMove = false;
            return;
        }
        windUp -= Time.deltaTime;
    }
}
