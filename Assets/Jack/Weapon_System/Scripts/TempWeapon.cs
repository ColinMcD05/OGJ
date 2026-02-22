public class TempWeapon : Weapon
{
    public float _durability;
    public enum Temporary
    {
        Sword = 0,
        Axe,
        Bow
    };

    public Temporary tempType;
    public static int TempEnumCount = System.Enum.GetValues(typeof(Temporary)).Length;
}