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
    GameObject player;
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
            player = collision.gameObject;
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
            Destroy(GameObject.Find("Enemies"));
            CalculateScore();
        }
    }

    void ChangeLevel()
    {
        player.transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    void CalculateScore()
    {
        foreach (int collectable in gameManager.currentCollectables)
        {
            gainedScore += gameManager.currentCollectables[0];
            gameManager.currentCollectables.RemoveAt(0);
        }

        Invoke("ChangeLevel", 3);

        foreach (TempWeapon.Temporary weaponType in playerController.weaponsDict)
        {
            gainedScore += playerController.weaponsDict[weaponType].Count * 100 * ((int)weaponType + 1);
        }

        canPrint = true;
    }

    void PrintScore()
    {
        gameManager.score += gainedScore;
    }
}
