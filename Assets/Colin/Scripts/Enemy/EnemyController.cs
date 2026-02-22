using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public int health;
    [HideInInspector] public int stunHealth;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip getHit;

    public void GetHit(int damage)
    {
        audioSource.PlayOneShot(getHit, 0.7f);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Stunned(int stunDamage)
    {
        audioSource.PlayOneShot(getHit, 0.7f);
        stunHealth -= stunDamage;
        if (stunHealth <= 0)
        {
            gameObject.GetComponent<EnemyMovement>().enabled = false;
            Invoke("Unstun", 5);
        }
    }

    void Unstun()
    {
        gameObject.GetComponent<EnemyMovement>().enabled = true;
    }
}
