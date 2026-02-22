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

        Invoke("ChangeLevel", 1);

        foreach (TempWeapon.Temporary weaponType in playerController.weaponsDict.Keys)
        {
            if ((int)weaponType < 4)
            {
                gainedScore += playerController.weaponsDict[weaponType + 1].Count * 100 * ((int)weaponType);
                playerController.weaponsDict[weaponType + 1].Clear();
            }
        }
        canPrint = true;
    }

    void PrintScore()
    {
        gameManager.score += gainedScore;
    }
}
