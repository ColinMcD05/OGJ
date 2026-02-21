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

    void Start()
    {
        _id = Random.Range(1000,10000); // Not shielding duplicates (!)
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Equipped by the Player!");
            transform.gameObject.SetActive(false);
        }
    }
}
