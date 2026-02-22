using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{

    float invincibilityFrames;

    private void Update()
    {
        if (invincibilityFrames > 0)
        {
            invincibilityFrames -= Time.deltaTime;
        }
    }

    public void Hit(int damage)
    {
        if (invincibilityFrames <= 0)
        {
            gameObject.GetComponent<PlayerController>().lives -= damage;
            invincibilityFrames = 4;
            if (gameObject.GetComponent<PlayerController>().lives <= 0)
            {
                Death();
            }
        }
    }
    public void Death()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
}

