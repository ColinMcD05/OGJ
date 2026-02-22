using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    public int speed = 5;
    public int damage;

    void Update()
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
