using UnityEngine;

public class Armor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().maxLives += 1;
            collision.GetComponent<PlayerController>().lives += 1;
            Destroy(this.gameObject);
        }
    }
}
