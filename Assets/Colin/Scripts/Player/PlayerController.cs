using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public int maxLives;
    public List<string> inventory;
    public List<TempWeapon> tWeapons;
    public List<PermWeapon> pWeapons;
    public bool inShadow;

    void Start()
    {
        lives = 3;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon temp = null;

        if(collision.gameObject.CompareTag("Weapon"))
        {
            // Debug.Log("Touched: " + collision.name);
            if(temp=collision.gameObject.GetComponent<TempWeapon>())
            {
                // Debug.Log("Temp Weapon Detected: " + collision.name);
                tWeapons.Add((TempWeapon)temp);
                // Debug.Log("Number of Temporary Weapons: " + tWeapons.Count);
            }
            else if(temp=collision.gameObject.GetComponent<PermWeapon>())
            {
                // Debug.Log("Perm Weapon Detected: " + collision.name);
                pWeapons.Add((PermWeapon)temp);
                // Debug.Log("Number of Permanent Weapons: " + pWeapons.Count);
            }
        }
    }
}
