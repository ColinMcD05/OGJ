using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public int maxLives;
    public List<string> inventory;
    [HideInInspector] public List<List<Weapon>> tWeapons = new List<List<Weapon>>();
    [HideInInspector] public List<List<Weapon>> pWeapons = new List<List<Weapon>>();
    [HideInInspector] public int activeTWeapon = 0;
    [HideInInspector] public int activePWeapon = 0;
    public bool inShadow;
    public bool isCaught;

    void Start()
    {
        lives = 3;

        // This is not an elegant solution
        for(int i=0;i<PermWeapon.PermEnumCount;i++) tWeapons.Add(new List<Weapon>());
        for(int i=0;i<TempWeapon.TempEnumCount;i++) pWeapons.Add(new List<Weapon>());
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
}
