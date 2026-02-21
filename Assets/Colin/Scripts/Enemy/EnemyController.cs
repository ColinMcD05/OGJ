using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public int health;
    [HideInInspector] public int stunHealth;

    public void GetHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
