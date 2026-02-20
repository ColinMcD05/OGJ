using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    private int score;

    [Header("Persistant Objects")]
    [SerializeField] GameObject[] persistantObjects;

    private void Awake()
    {
       if (instance != null)
        {
            CleanAndDestroy();
            return;
        }
       else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkObjects();
        }
    }

    private void MarkObjects()
    {
        foreach (GameObject obj in persistantObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanAndDestroy()
    {
        foreach (GameObject obj in persistantObjects)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }

    public void AddScore(int gainedScore)
    {
        score += gainedScore;
    }
}
