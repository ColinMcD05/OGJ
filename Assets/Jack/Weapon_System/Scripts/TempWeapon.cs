using UnityEngine;

public class TempWeapon : Weapon
{
    public float _durability;
    public enum Temporary
    {
        Sword = 0,
        Bow,
        Axe,
    };

    public Temporary tempType;
    public static int TempEnumCount = System.Enum.GetValues(typeof(Temporary)).Length;

    void OnTriggerEnter2D(Collider2D col)
    {
        EnemyController enemy;
        if(enemy=col.GetComponent<EnemyController>())
        {
            enemy.GetHit(_damage);
        }
    }
}