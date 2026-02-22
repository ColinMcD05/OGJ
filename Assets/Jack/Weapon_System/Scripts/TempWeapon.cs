using System;
using UnityEngine;

public class TempWeapon : Weapon
{
    public float _durability;
    public enum Temporary
    {
        Club=0,
        Sword,
        Bow,
        Axe,
    };

    public Temporary tempType;
    public static int TempEnumCount = System.Enum.GetValues(typeof(Temporary)).Length;
    public static event Action durabilityDown;

    void OnTriggerEnter2D(Collider2D col)
    {
        EnemyController enemy;
        if(enemy=col.GetComponent<EnemyController>())
        {
            enemy.GetHit(_damage);
            enemy.Stunned(_stun);
            _durability-=1;
        }
    }
}