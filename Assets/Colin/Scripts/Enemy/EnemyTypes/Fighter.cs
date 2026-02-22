using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Fighter : MonoBehaviour
{
    GameObject player;
    [SerializeField] EnemyController enemyController;
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] GameObject eyes;

    public int health;
    public int stunHealth;
    public float range = 5;
    public float cooldownTimer = 5;
    public float windUp = 1;
    public float chargeTime;
    public float chargeSpeed;
    private float timer;
    public int damage;

    Transform target;

    void Awake()
    {
        enemyController.health = health;
        enemyController.stunHealth = stunHealth;
        chargeSpeed = enemyMovement.moveSpeed * 1.5f;
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
                Invoke("SetTarget", windUp);
                Invoke("Attack", windUp);
                cooldownTimer = 5;
            }
        }
        else if (cooldownTimer > 0 && timer == 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (timer <= chargeTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, chargeSpeed * Time.deltaTime);
        }
        else
        {
            Invoke("CanMove", 1);
            return;
        }
    }

    void SetTarget()
    {
        target = player.transform;
        timer = 0;
    }

    void CanMove()
    {
        enemyMovement.canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (timer <= chargeTime)
            {
                timer = chargeTime;
                collision.gameObject.GetComponent<PlayerDeath>().Hit(damage);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerDeath>().Hit(damage/2);
            }
        }
        else
        {
            timer = chargeTime;
        }
    }
}
