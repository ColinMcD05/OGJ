using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    GameManager gameManager;
    bool canPrint = false;
    int gainedScore;
    float amountAdded;
    PlayerController playerController;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (canPrint)
        {
            PrintScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
            Destroy(GameObject.Find("Enemies"));
            CalculateScore();
        }
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>().enabled = false;
    }

    void CalculateScore()
    {
        foreach (int collectable in gameManager.currentCollectables)
        {
            gainedScore += gameManager.currentCollectables[0];
            gameManager.currentCollectables.RemoveAt(0);
        }
        /* Gain score based on items
        foreach (int collectabel in gameManager.)
        {

        }
        canPrint = true;
        */
    }

    void PrintScore()
    {
        gameManager.score += gainedScore;
    }
}
