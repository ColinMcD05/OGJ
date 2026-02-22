using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float waitTime;

    private void Awake()
    {
        Invoke("RestartGame", waitTime);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
