using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    
    public void Hit()
    {
        gameObject.GetComponent<PlayerController>().lives--;
        if (gameObject.GetComponent<PlayerController>().lives-- == 0)
        {
            Death();
        }
    }
    public void Death()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
}

