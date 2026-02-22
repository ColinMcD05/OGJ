using UnityEngine;

public class Kobold : MonoBehaviour
{
    GameObject player;
    [SerializeField] EnemyController enemyController;
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] GameObject eyes;

    public int health;
    public int stunHealth;
    public float windUp = 1;
    public int damage = 1;

    void Awake()
    {
        enemyController.health = health;
        enemyController.stunHealth = stunHealth;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Attack()
    {
        player.GetComponent<PlayerDeath>().Hit(damage);
        enemyMovement.canMove = false;
        Invoke("CanMove", 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
    void CanMove()
    {
        enemyMovement.canMove = true;
    }
}
