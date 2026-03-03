using UnityEngine;
using UnityEngine.AI;

public class Archer : MonoBehaviour
{
    GameObject player;
    [SerializeField] EnemyController enemyController;
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject arrowPrefab;

    public int health;
    public int stunHealth;
    public float range = 5;
    public float cooldownTimer = 5;
    public float windUp = 1;

    void Awake()
    {
        enemyController.health = health;
        enemyController.stunHealth = stunHealth;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (enemyMovement.seePlayer && cooldownTimer <= 0)
        {
            // Play attack animation
            if (Vector3.Distance(player.transform.position, transform.position) < range)
            {
                enemyMovement.canMove = false;
                Invoke("Attack", windUp);
                cooldownTimer = 7;
            }
        }
        else if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void Attack()
    {
        // shoot animation
        // back to normal anitmation
        GameObject arrow = Instantiate(arrowPrefab, transform.position, eyes.transform.rotation);
        GetComponent<NavMeshAgent>().isStopped = true;
        Invoke("CanMove", 1);
    }

    void CanMove()
    {
        enemyMovement.canMove = true;
        GetComponent<NavMeshAgent>().isStopped = true;
    }
}
