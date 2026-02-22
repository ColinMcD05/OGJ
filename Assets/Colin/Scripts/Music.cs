using UnityEngine;

public class Music : MonoBehaviour
{

    PlayerController player;
    AudioSource audioSource;
    AudioClip intro, main, stinger, alarm;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        
    }
}
