using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int collectableScore;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddCollectable(collectableScore);
            Destroy(this.gameObject);
        }
    }
}
