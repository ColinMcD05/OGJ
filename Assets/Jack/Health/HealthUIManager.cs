using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager: MonoBehaviour
{
    public GameObject[] healthSRList;
    public GameObject[] nohealthSRList;
    public PlayerController playerController;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        for (int i=4; i>playerController.lives-1;i--)
        {
            healthSRList[i].SetActive(false);
        }
    }
}
