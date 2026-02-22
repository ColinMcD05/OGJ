using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Weapon : MonoBehaviour
{
    // Weapon GameObject Data
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D bc;


    // Weapon Data
    public float _damage;
    public float _stun;
    public float _cooldown;
    public string _name;
    public int _rank;
    public int _id;
    bool _pickedUp = false;


    public void PickedUp()
    {
        _pickedUp = true;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        _id = Random.Range(1000,10000); // Not shielding duplicates (!)
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(
            (
                gameObject.tag!="Player"
                && col.GetComponent<TempWeapon>()==null
            )
            && col.CompareTag("Player"))
        {
            transform.gameObject.SetActive(false);
        }
    }
}
