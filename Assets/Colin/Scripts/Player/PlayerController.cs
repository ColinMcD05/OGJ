using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public List<string> inventory;

    void Start()
    {
        lives = 3;
    }
}
