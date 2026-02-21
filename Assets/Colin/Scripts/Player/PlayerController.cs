using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public int maxLives;
    public List<string> inventory;
    List<List<TempWeapon>> tWeapons = new List<List<TempWeapon>>();
    List<List<PermWeapon>> pWeapons = new List<List<PermWeapon>>();
    int activeTWeapon = 0;
    int activePWeapon = 0;
    public bool inShadow;

    void Start()
    {
        lives = 3;

        // This is not an elegant solution
        for(int i=0;i<4;i++) tWeapons.Add(new List<TempWeapon>());
        pWeapons.Add(new List<PermWeapon>());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon temp = null;

        /// Picking Up Weapon Logic ///
        if(collision.gameObject.CompareTag("Weapon"))
        {
            // Debug.Log("Touched: " + collision.name);
            if(temp=collision.gameObject.GetComponent<TempWeapon>())
            {
                int tempIdx = (int)((TempWeapon)temp).tempType;
                // Debug.Log("Temp Weapon Detected: " + collision.name);
                tWeapons[tempIdx].Add((TempWeapon)temp);
                Debug.Log("Number of Temporary Weapons: " + tWeapons[tempIdx].Count);
            }
            else if(temp=collision.gameObject.GetComponent<PermWeapon>())
            {
                int permIdx = (int)((PermWeapon)temp).permType;
                // Debug.Log("Perm Weapon Detected: " + collision.name);
                // pWeapons.Add((PermWeapon)temp);
                pWeapons[permIdx].Add((PermWeapon)temp);
                // Debug.Log("Number of Permanent Weapons: " + pWeapons.Count);
                Debug.Log("Number of Permanent Weapons: " + pWeapons[permIdx].Count);
            }
        }
    }

    // Player Input Callback(s)
    public void OnToggleTempWeapon(InputValue value)
    {
        activeTWeapon++;
        activeTWeapon%=4; // Hardcoded (!)
        Debug.Log("Toggle TempWeapon: " + activeTWeapon);
    }

    public void OnTogglePermWeapon(InputValue value)
    {
        activePWeapon++;
        activePWeapon%=4; // Hardcoded (!)
        Debug.Log("Toggle PermWeapon: " + activePWeapon);
    }
}
