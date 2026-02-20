using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] float _stun;
    [SerializeField] float _cooldown;
    [SerializeField] string _name;
    [SerializeField] int _rank;
    int _id;
    bool _pickedUp = false;

    public void PickedUp()
    {
        _pickedUp = true;
    }
}
