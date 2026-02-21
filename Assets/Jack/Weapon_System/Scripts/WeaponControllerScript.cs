using UnityEngine;

public class WeaponControllerScript : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
