using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 target;
    GameObject player;
    [SerializeField] GameObject miniFireballPrefab;

    public int speed = 5;
    public int damage = 2;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (transform.localScale.x <= 3)
        {
            transform.localScale += new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime;
            target = player.transform.position;
        }
        else if (transform.position != target)
        {
            Movement();
        }
        else
        {
            HitSomething();
        }
            Spin();
    }
    void Spin()
    {
        transform.rotation *= Quaternion.Euler(0, 0, 1);
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!gameObject.GetComponent<Collider2D>().enabled)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    void HitSomething()
    {
        Destroy(gameObject);

        for (int i = 0; i < 4; i++)
        {
            Instantiate(miniFireballPrefab, transform.position, Quaternion.Euler(0, 0, 45 + 90 * i));
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
        HitSomething();
    }
}
