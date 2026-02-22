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
    [SerializeField] float _damage;
    [SerializeField] float _stun;
    [SerializeField] float _cooldown;
    [SerializeField] string _name;
    [SerializeField] int _rank;
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
        if(gameObject.tag!="Player" && col.CompareTag("Player"))
        {
            // Debug.Log("Equipped by the Player!");
            // This is so slop!
            // SpriteRenderer targetSR = col.GetComponent<PlayerInput>()
            //     .weaponControllerScript
            //     .hitbox.GetComponent<SpriteRenderer>();
            // Alter the currently held weapon for the player, which is terrible here!
            // targetSR.sprite = GetComponent<SpriteRenderer>().sprite;
            // targetSR.color = GetComponent<SpriteRenderer>().color;

            transform.gameObject.SetActive(false);
        }
    }
}
