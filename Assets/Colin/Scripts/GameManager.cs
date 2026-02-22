using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    public int score;
    [HideInInspector] public List<int> currentCollectables;
    [HideInInspector] public Dictionary<string, int> collectedItems;

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

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            CleanAndDestroy();
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

    public void AddCollectable(int collectableScore)
    {
        currentCollectables.Add(collectableScore);
    }

    public void AddScore(int gainedScore)
    {
        score += gainedScore;
    }
}
