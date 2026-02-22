using UnityEngine;

public class Shoes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().walkSpeed += 1;
            collision.GetComponent<PlayerMovement>().sprintSpeed += 1;
            Destroy(this.gameObject);
        }
    }
}
