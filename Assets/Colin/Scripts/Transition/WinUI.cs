using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI scoreText;
    public float waitTime;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreText.text = Convert.ToString(gameManager.score);
        Invoke("RestartGame", waitTime);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
