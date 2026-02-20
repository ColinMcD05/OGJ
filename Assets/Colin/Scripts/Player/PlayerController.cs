using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public List<string> inventory;
    public bool inShadow;

    void Start()
    {
        lives = 3;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Touched: " + collision.name);
        }
    }
}
