using UnityEngine;

public class WeaponControllerScript : MonoBehaviour
{
    public Animator anim;
    public GameObject hitbox;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
