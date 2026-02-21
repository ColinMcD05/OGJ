using UnityEngine;

public class Wizard : MonoBehaviour
{
    GameObject player;
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject fireballPrefab;

    public int health;
    public int stunHealth;
    public float range = 10;
    public float cooldownTimer = 5;
    public float windUp = 1;

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
                Attack();
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
        GameObject fireball = Instantiate(fireballPrefab, transform.position, eyes.transform.rotation);

        Invoke("CanMove", windUp + 1);
    }

    void CanMove()
    {
        enemyMovement.canMove = true;
    }
}
