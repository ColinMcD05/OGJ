using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    [Header("Persistant Objects")]
    [SerializeField] GameObject[] persistantObjects;

    private void Awake()
    {
       if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
       else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void MarkObjects()
    {
        foreach (GameObject obj in persistantObjects)
        {

        }
    }
}
