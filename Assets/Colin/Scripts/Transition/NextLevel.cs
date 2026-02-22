using System.Collections.Generic;
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
        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
        GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>().enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void CalculateScore()
    {
        for (int collectable =0; collectable < gameManager.currentCollectables.Count; collectable ++)
        {
            gainedScore += gameManager.currentCollectables[collectable];
        }
        gameManager.currentCollectables.Clear();
        /*
        foreach (TempWeapon.Temporary weaponType in playerController.weaponsDict.Keys)
        {
            if ((int)weaponType < 4)
            {
                gainedScore += playerController.weaponsDict[weaponType + 1].Count * 100 * ((int)weaponType);
                playerController.weaponsDict[weaponType + 1].Clear();
            }
        }
        */
        ChangeLevel();
    }

    void PrintScore()
    {
        gameManager.score += gainedScore;
    }
}
