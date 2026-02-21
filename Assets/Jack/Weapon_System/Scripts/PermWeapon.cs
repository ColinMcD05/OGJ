public class PermWeapon : Weapon
{
    public enum Permanent
    {
        Club = 0,
    };

    public Permanent permType;
    public static int PermEnumCount = System.Enum.GetValues(typeof(Permanent)).Length;
}
