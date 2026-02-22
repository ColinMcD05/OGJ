using UnityEngine;

public class Minifireball : MonoBehaviour
{
    public int damage = 1;
    public int speed;

    private void Awake()
    {
        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
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
