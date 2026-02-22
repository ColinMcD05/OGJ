using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    GameObject player;
    public float speed;
    public int damage = 1;
    public float rotationSpeed;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Transform target = player.transform;

        transform.position += Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDeath>().Hit(damage);
        }
        else if (collision.gameObject.layer == LayerMask.GetMask("Enemies"))
        {
            collision.GetComponent<EnemyController>().GetHit(damage);
        }
            Destroy(this.gameObject);
    }
}
