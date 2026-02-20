using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public int maxLives;
    public List<string> inventory;
    public bool inShadow;

    void Start()
    {
        lives = 3;
    }
}
