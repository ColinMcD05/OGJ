using UnityEngine;

public class Minifireball : MonoBehaviour
{

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
            collision.GetComponent<PlayerDeath>().Hit();
        }
        Destroy(this.gameObject);
    }
}
