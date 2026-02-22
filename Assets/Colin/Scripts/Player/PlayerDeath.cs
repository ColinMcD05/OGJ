using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hurt;
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
        audioSource.PlayOneShot(hurt, 0.6f);
        if (invincibilityFrames <= 0)
        {
            gameObject.GetComponent<PlayerController>().lives -= damage;
            invincibilityFrames = 4;
            if (gameObject.GetComponent<PlayerController>().lives <= 0)
            {

                Invoke("Death", 0);
            }
        }
    }
    public void Death()
    {
        SceneManager.LoadScene(7);
    }
}

