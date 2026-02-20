using UnityEngine;

public class TempWeapon : Weapon
{
    [SerializeField] float _durability;
    public enum Temporary
    {
        Sword = 0,
        Axe,
        Bow
    };

    public Temporary tempType;
}